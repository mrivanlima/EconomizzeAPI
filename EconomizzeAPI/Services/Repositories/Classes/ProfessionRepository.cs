using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class ProfessionRepository : IProfessionRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;

        private StatusHelper status;

        #region CONSTRUCTOR
        public ProfessionRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            status = new StatusHelper();
        }
        #endregion

        #region CREATE PROFESSION IN DB
        public async Task<Tuple<Profession, StatusHelper>> CreateProfessionAsync(Profession profession)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_profession_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_out_profession_id", profession.ProfessionId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_profession_name", profession.ProfessionName);
                cmd.Parameters.AddWithValue("p_created_by", profession.CreatedBy);
                cmd.Parameters.AddWithValue("p_modified_by", profession.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                status.HasError = (bool)cmd.Parameters["p_error"].Value;
                if (!status.HasError)
                {
                    profession.ProfessionId = (short)cmd.Parameters["p_out_profession_id"].Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                status.HasError = true;
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return new Tuple<Profession, StatusHelper>(profession, status);
        }
        #endregion

        #region READ ALL PROFESSIONS IN DB
        public async Task<IEnumerable<Profession>> ReadAllProfessionsAsync()
        {
            ICollection<Profession> professions = new List<Profession>();
            await using var command = new NpgsqlCommand("SELECT * FROM app.usp_api_profession_read_all()", _connection);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    professions.Add(new Profession
                    {
                        ProfessionId = reader.GetInt16(0),
                        ProfessionName = reader.GetString(1),
                        ProfessionNameAscii = reader.GetString(2),
                        CreatedBy = reader.GetInt32(3),
                        CreatedOn = reader.GetDateTime(4),
                        ModifiedBy = reader.GetInt32(5),
                        ModifiedOn = reader.GetDateTime(6)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"status: {ex.Message}");
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return professions;
        }
        #endregion
    }
}
