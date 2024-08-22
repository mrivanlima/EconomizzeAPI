using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
	public class UserLoginRepository : IUserLoginRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;
		private ErrorHelper error;

		public UserLoginRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
			error = new ErrorHelper();
		}

		public async Task<Tuple<RegisterViewModel, ErrorHelper>> CreateAsync(RegisterViewModel register)
		{
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_login_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				//postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
				cmd.Parameters.AddWithValue("p_username", register.Username);
				cmd.Parameters.AddWithValue("p_user_unique_id", register.UserUniqueId);
				cmd.Parameters.AddWithValue("p_password_hash", register.Password);
				cmd.Parameters.AddWithValue("p_password_salt", register.Password);

				cmd.Parameters.AddWithValue("p_error", error.HasError).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.AddWithValue("p_out_user_id", register.UserId).Direction = ParameterDirection.Output;
				cmd.Parameters.AddWithValue("p_out_message", error.ErrorMessage).Direction = ParameterDirection.Output;
				await _connection.OpenAsync();

				cmd.ExecuteNonQuery();

				error.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
				error.ErrorMessage = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
				if (!error.HasError)
				{
					register.UserId = Convert.ToInt32(cmd.Parameters["p_out_user_id"].Value);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				await _connection.CloseAsync();
			}
			return new Tuple<RegisterViewModel, ErrorHelper>(register, error);
		}

		public async Task<UserLogin> ReadUserByUserName(UserLoginViewModel login)
		{
			NpgsqlDataReader? npgsqlDr = null;
			NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.usp_api_user_login_read(@username)", _connection);
			UserLogin userLogin = new UserLogin();
			cmd.Parameters.AddWithValue("@username", login.Username); 

			try
			{
				await _connection.OpenAsync();
				npgsqlDr = await cmd.ExecuteReaderAsync();

				if (await npgsqlDr.ReadAsync())
				{
					userLogin.UserId = npgsqlDr.GetInt32(npgsqlDr.GetOrdinal("user_id"));
					userLogin.UserUniqueId = npgsqlDr.GetGuid(npgsqlDr.GetOrdinal("user_unique_id"));
					userLogin.Username = login.Username;
					userLogin.PasswordHash = npgsqlDr.GetString(npgsqlDr.GetOrdinal("password_hash"));
					userLogin.PasswordSalt = npgsqlDr.GetString(npgsqlDr.GetOrdinal("password_salt"));
					userLogin.IsVerified = npgsqlDr.GetBoolean(npgsqlDr.GetOrdinal("is_verified"));
					userLogin.IsActive = npgsqlDr.GetBoolean(npgsqlDr.GetOrdinal("is_active"));
					userLogin.IsLocked = npgsqlDr.GetBoolean(npgsqlDr.GetOrdinal("is_locked"));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				await _connection.CloseAsync();
				if (npgsqlDr != null)
				{
					await npgsqlDr.CloseAsync();
				}
			}
			return userLogin;
		}

        

        public async Task<ErrorHelper> UserVerifyAsync(int userId, Guid userUniqueId)
		{
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_login_confirm_update", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_id", userId);
                cmd.Parameters.AddWithValue("p_user_unique_id", userUniqueId);
                cmd.Parameters.AddWithValue("p_error", error.HasError).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.AddWithValue("p_out_message", error.ErrorMessage).Direction = ParameterDirection.Output;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

                error.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
                error.ErrorMessage = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return error;
		}
    }
}
