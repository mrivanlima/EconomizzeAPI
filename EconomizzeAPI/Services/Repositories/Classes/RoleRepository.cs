using Economizze.Library;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;

		public RoleRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
		}
		public async Task<Tuple<Role, bool>> CreateAsync(Role role)
		{
			bool error = false;
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_role_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("p_out_role_id", role.RoleId).Direction = ParameterDirection.Output;
				cmd.Parameters.AddWithValue("p_role_name", role.RoleName);
				cmd.Parameters.AddWithValue("p_created_by", role.CreatedBy);
				cmd.Parameters.AddWithValue("p_modified_by", role.ModifiedBy);
				cmd.Parameters.AddWithValue("p_error", error).Direction = ParameterDirection.InputOutput;
				await _connection.OpenAsync();

				await cmd.ExecuteNonQueryAsync();

				error = (bool)cmd.Parameters["p_error"].Value;
				if (!error)
				{
					role.RoleId = (short)cmd.Parameters["p_out_group_id"].Value;
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

			return new Tuple<Role, bool>(role, error);
		}

		public async Task<IEnumerable<Role>> ReadAllAsync()
		{
			ICollection<Role> roles = new List<Role>();
			await using var command = new NpgsqlCommand("SELECT * FROM app.usp_api_role_read_all()", _connection);

			try
			{
				await _connection.OpenAsync();
				await using var reader = await command.ExecuteReaderAsync();

				while (await reader.ReadAsync())
				{
					roles.Add(new Role
					{
						RoleId = reader.GetInt16(0),
						RoleName = reader.GetString(1),
						RoleNameAscii = reader.GetString(2),
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
			return roles;
		}

		public Task<Role> ReadByIdAsync(short id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(Role role)
		{
			throw new NotImplementedException();
		}
	}
}
