using Economizze.Library;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;

        public GroupRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
        }
        public async Task<Tuple<Group, bool>> CreateAsync(Group group)
        {
            bool error = false;
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_group_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_out_group_id", group.GroupId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_group_name", group.GroupName);
                cmd.Parameters.AddWithValue("p_created_by", group.CreatedBy);
                cmd.Parameters.AddWithValue("p_modified_by", group.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", error).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                error = (bool)cmd.Parameters["p_error"].Value;
                if (!error)
                {
                    group.GroupId = (short)cmd.Parameters["p_out_group_id"].Value;
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

            return new Tuple<Group, bool>(group, error);
        }

        public async Task<IEnumerable<Group>> ReadAllAsync()
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
                        GroupNameAscii = reader.GetString(2)
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
            return groups;
        }

        public Task<Group> ReadByIdAsync(short id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
