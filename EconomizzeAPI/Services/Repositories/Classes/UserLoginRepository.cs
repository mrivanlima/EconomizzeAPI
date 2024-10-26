using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
	public class UserLoginRepository : IUserLoginRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;

		private StatusHelper status;

        #region CONSTRUCTOR
        public UserLoginRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
			status = new StatusHelper();
		}
        #endregion

        #region REGISTER NEW USER
        public async Task<Tuple<RegisterViewModel, StatusHelper>> CreateUserLoginAsync(RegisterViewModel register)
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

				cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.AddWithValue("p_out_user_id", register.UserId).Direction = ParameterDirection.Output;
				cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
				await _connection.OpenAsync();

				cmd.ExecuteNonQuery();

				status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
				status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
				if (!status.HasError)
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
			return new Tuple<RegisterViewModel, StatusHelper>(register, status);
		}
        #endregion

        #region CHANGE PASSWORD FOR LOGGED IN USER
        public async Task<Tuple<LoggedInPasswordViewModel, StatusHelper>> ChangeUserPassword(LoggedInPasswordViewModel userLogin)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_login_update_password", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
                cmd.Parameters.AddWithValue("p_user_id", userLogin.UserId);
                cmd.Parameters.AddWithValue("p_password_hash", userLogin.NewPassword);
                cmd.Parameters.AddWithValue("p_password_salt", userLogin.NewPassword);

                cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

                status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
                status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
                //if (!status.HasError)
                //{
                //    register.UserId = Convert.ToInt32(cmd.Parameters["p_out_user_id"].Value);
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return new Tuple<LoggedInPasswordViewModel, StatusHelper>(userLogin, status);
        }
        #endregion

        #region CHANGE PASSWORD FOR NOT LOGGED IN USER
        public async Task<Tuple<ForgotPasswordViewModel, StatusHelper>> ChangeUserForgotPassword(ForgotPasswordViewModel userLogin)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_login_update_password", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
                cmd.Parameters.AddWithValue("p_user_id", userLogin.UserId);
                cmd.Parameters.AddWithValue("p_password_hash", userLogin.NewPassword);
                cmd.Parameters.AddWithValue("p_password_salt", userLogin.NewPassword);

                cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

                status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
                status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
                //if (!status.HasError)
                //{
                //    register.UserId = Convert.ToInt32(cmd.Parameters["p_out_user_id"].Value);
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return new Tuple<ForgotPasswordViewModel, StatusHelper>(userLogin, status);
        }
        #endregion

        #region READ ID AND UUID
        public async Task<ForgotPasswordViewModel> ReadIdUuid(ForgotPasswordViewModel forgotPassword)
        {
            NpgsqlDataReader? npgsqlDr = null;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.usp_api_user_login_read(@username)", _connection);
            UserLogin userLogin = new UserLogin();
            cmd.Parameters.AddWithValue("@username", forgotPassword.Username);

            try
            {
                await _connection.OpenAsync();
                npgsqlDr = await cmd.ExecuteReaderAsync();

                if (await npgsqlDr.ReadAsync())
                {
                    userLogin.UserId = npgsqlDr.GetInt32(npgsqlDr.GetOrdinal("user_id"));
                    userLogin.UserUniqueId = npgsqlDr.GetGuid(npgsqlDr.GetOrdinal("user_unique_id"));
                }

                forgotPassword.UserId = userLogin.UserId;
                forgotPassword.UserUniqueId = userLogin.UserUniqueId;
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
            return forgotPassword;
        }
        #endregion

        #region VERIFY USER
        public async Task<StatusHelper> UserVerifyAsync(int userId, Guid userUniqueId)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_login_confirm_update", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_id", userId);
                cmd.Parameters.AddWithValue("p_user_unique_id", userUniqueId);
                cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

                status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
                status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return status;
        }
        #endregion

        #region READ USER BY USERNAME
        public async Task<UserLogin> ReadUserLoginByUserName(UserLoginViewModel login)
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
        #endregion

        #region READ BY ID
        public async Task<UserLogin> ReadByIdAsync(int id)
        {
            UserLogin? userLogin = null;
            NpgsqlDataReader? npgsqlDr = null;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.usp_api_user_login_read_by_id(@p_user_id)", _connection);

            cmd.Parameters.AddWithValue("@p_user_id", id);

            try
            {
                await _connection.OpenAsync();
                npgsqlDr = await cmd.ExecuteReaderAsync();

                if (await npgsqlDr.ReadAsync())
                {
                    userLogin = new();
                    userLogin.UserId = npgsqlDr.GetInt32(npgsqlDr.GetOrdinal("user_id"));
                    userLogin.UserUniqueId = npgsqlDr.GetGuid(npgsqlDr.GetOrdinal("user_unique_id"));
                    userLogin.Username = npgsqlDr.GetString(npgsqlDr.GetOrdinal("username"));
                    userLogin.PasswordHash = npgsqlDr.GetString(npgsqlDr.GetOrdinal("password_hash"));
                    userLogin.PasswordSalt = npgsqlDr.GetString(npgsqlDr.GetOrdinal("password_salt"));
                    userLogin.IsVerified = npgsqlDr.GetBoolean(npgsqlDr.GetOrdinal("is_verified"));
                    userLogin.IsActive = npgsqlDr.GetBoolean(npgsqlDr.GetOrdinal("is_active"));
                    userLogin.IsLocked = npgsqlDr.GetBoolean(npgsqlDr.GetOrdinal("is_locked"));
                    userLogin.PasswordAttempts = npgsqlDr.GetInt16(npgsqlDr.GetOrdinal("password_attempts"));
                    userLogin.ChangedInitialPassword = npgsqlDr.GetBoolean(npgsqlDr.GetOrdinal("changed_initial_password"));
                    //userLogin.LockedTime = npgsqlDr.GetDateTime(npgsqlDr.GetOrdinal("locked_time"));
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
        #endregion
    }
}
