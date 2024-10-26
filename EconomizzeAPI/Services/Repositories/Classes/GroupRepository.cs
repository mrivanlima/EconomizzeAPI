using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;
using System.Net.NetworkInformation;

namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;

        private StatusHelper status;

        #region CONSTRUCTOR
        public GroupRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            status = new StatusHelper();
        }
        #endregion

        #region CREATE GROUP IN DB
        public async Task<Tuple<Group, StatusHelper>> CreateGroupAsync(Group group)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_group_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_out_group_id", group.GroupId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_group_name", group.GroupName);
                cmd.Parameters.AddWithValue("p_created_by", group.CreatedBy);
                cmd.Parameters.AddWithValue("p_modified_by", group.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                status.HasError = (bool)cmd.Parameters["p_error"].Value;
                if (!status.HasError)
                {
                    group.GroupId = (short)cmd.Parameters["p_out_group_id"].Value;
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

            return new Tuple<Group, StatusHelper>(group, status);
        }
        #endregion

        #region READ ALL GROUPS IN DB
        public async Task<IEnumerable<Group>> ReadAllGroupsAsync()
        {
            ICollection<Group> groups = new List<Group>();
            await using var command = new NpgsqlCommand("SELECT * FROM app.usp_api_group_read_all()", _connection);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    groups.Add(new Group
                    {
                        GroupId = reader.GetInt16(0),
                        GroupName = reader.GetString(1),
                        GroupNameAscii = reader.GetString(2),
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
            return groups;
        }
        #endregion
    }
}
