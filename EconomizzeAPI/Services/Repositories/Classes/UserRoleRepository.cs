using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
	public class UserRoleRepository : IUserRoleRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;

		public StatusHelper status;

        #region CONSTRUCTOR
        public UserRoleRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
			status = new StatusHelper();
		}
        #endregion

        #region CREATE USERROLE IN DB
        public async Task<Tuple<UserRole, StatusHelper>> CreateUserRoleAsync(UserRole userRole)
		{
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_role_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("p_out_role_id", userRole.RoleId).Direction = ParameterDirection.Output;
				cmd.Parameters.AddWithValue("p_user_id", userRole.UserId);
				cmd.Parameters.AddWithValue("p_is_active", userRole.IsActive);
				cmd.Parameters.AddWithValue("p_created_by", userRole.CreatedBy);
				cmd.Parameters.AddWithValue("p_modified_by", userRole.ModifiedBy);
				cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
				await _connection.OpenAsync();

				await cmd.ExecuteNonQueryAsync();

                status.HasError = (bool)cmd.Parameters["p_error"].Value;
				if (!status.HasError)
				{
					userRole.RoleId = (short)cmd.Parameters["p_out_role_id"].Value;
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

			return new Tuple<UserRole, StatusHelper>(userRole, status);
		}
        #endregion

        #region READ ALL USER ROLES IN DB
        public async Task<IEnumerable<Role>> ReadAllUserRolesAsync(UserRoleViewModel userRole)
		{
			NpgsqlDataReader? npgsqlDr = null;
			NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.usp_api_user_role_read(@userId)", _connection);
			ICollection<Role> roles = new List<Role>();
			cmd.Parameters.AddWithValue("@userId", userRole.UserId);

			try
			{
				await _connection.OpenAsync();
				npgsqlDr = await cmd.ExecuteReaderAsync();

				while (await npgsqlDr.ReadAsync())
				{
					roles.Add(new Role
					{
						RoleName = npgsqlDr.GetString(0),
						//RoleId = npgsqlDr.GetInt16(0),
						//UserId = npgsqlDr.GetInt32(1),
						//IsActive = npgsqlDr.GetBoolean(2),
						////RoleStartDate = npgsqlDr.GetDateTime(4),
						////RoleEndDate = npgsqlDr.GetDateTime(5),
						//CreatedBy = npgsqlDr.GetInt32(6),
						////CreatedOn = npgsqlDr.GetDateTime(7),
						//ModifiedBy = npgsqlDr.GetInt32(8)
						////ModifiedOn = npgsqlDr.GetDateTime(9)
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
			return roles;
		}
        #endregion
    }
}
