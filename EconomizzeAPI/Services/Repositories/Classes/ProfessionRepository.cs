using Economizze.Library;
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

        public ProfessionRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
        }
        public async Task<Tuple<Profession, bool>> CreateAsync(Profession profession)
        {
            bool error = false;
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_profession_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_out_profession_id", profession.ProfessionId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_profession_name", profession.ProfessionName);
                cmd.Parameters.AddWithValue("p_created_by", profession.CreatedBy);
                cmd.Parameters.AddWithValue("p_modified_by", profession.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", error).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                error = (bool)cmd.Parameters["p_error"].Value;
                if (!error)
                {
                    profession.ProfessionId = (short)cmd.Parameters["p_out_profession_id"].Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                error = true;
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return new Tuple<Profession, bool>(profession, error);
        }

        public async Task<IEnumerable<Profession>> ReadAllAsync()
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
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return professions;
        }

        public Task<Profession> ReadByIdAsync(short id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Profession profession)
        {
            throw new NotImplementedException();
        }
    }
}
