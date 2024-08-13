using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using System.Data;
using System.Diagnostics;
using System.Reflection.Emit;

namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;
        private ErrorHelper error;

        public UserRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            error = new ErrorHelper();
        }

        public async Task<Tuple<User, ErrorHelper>> CreateAsync(User user)
        {
            error.HasError = false;
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("p_date_of_birth", user.DateOfBirth.HasValue ? user.DateOfBirth.Value : DBNull.Value);
                //postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
                cmd.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                cmd.Parameters.AddWithValue("p_user_id", user.UserId);
                cmd.Parameters.AddWithValue("p_user_first_name", user.UserFirstName);
                cmd.Parameters.AddWithValue("p_user_middle_name", user.UserMiddleName ?? string.Empty);
                cmd.Parameters.AddWithValue("p_user_last_name", user.UserLastName ?? string.Empty);
                cmd.Parameters.AddWithValue("p_phone_number", user.PhoneNumber ?? string.Empty);
                cmd.Parameters.AddWithValue("p_cpf", user.Cpf ?? string.Empty);
                cmd.Parameters.AddWithValue("p_rg", user.Rg ?? string.Empty);
                cmd.Parameters.AddWithValue("p_created_by", user.CreatedBy.HasValue ? user.CreatedBy.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("p_modified_by", user.ModifiedBy.HasValue ? user.ModifiedBy.Value : DBNull.Value);

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
            return new Tuple<User, ErrorHelper>(user, error);
        }

        public async Task<User> ReadByIdAsync(int id)
        {
            User user = new();
            NpgsqlDataReader? npgsqlDr = null;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.usp_api_user_read_by_id(@p_user_id)", _connection);
            
            cmd.Parameters.AddWithValue("@p_user_id", id);

            try
            {
                await _connection.OpenAsync();
                npgsqlDr = await cmd.ExecuteReaderAsync();

                if (await npgsqlDr.ReadAsync())
                {
                    user.UserFirstName = npgsqlDr.GetString(npgsqlDr.GetOrdinal("user_first_name"));
                    user.UserMiddleName = npgsqlDr.GetString(npgsqlDr.GetOrdinal("user_middle_name"));
                    user.UserLastName = npgsqlDr.GetString(npgsqlDr.GetOrdinal("user_last_name"));
                    user.Cpf = npgsqlDr.GetString(npgsqlDr.GetOrdinal("cpf"));
                    user.Rg = npgsqlDr.GetString(npgsqlDr.GetOrdinal("rg"));
                    user.DateOfBirth = npgsqlDr.GetDateTime(npgsqlDr.GetOrdinal("date_of_birth"));
                    user.PhoneNumber = npgsqlDr.GetString(npgsqlDr.GetOrdinal("phone_number"));
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

            return user;
        }

        public async Task<UserDetailViewModel> ReadUserByUsername(string username)
        {
            NpgsqlDataReader? npgsqlDr = null;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.v_user_login_details WHERE username = @Username LIMIT 1", _connection);
            UserDetailViewModel userDetail = null;
            cmd.Parameters.AddWithValue("@Username", username);

            try
            {
                await _connection.OpenAsync();
                npgsqlDr = await cmd.ExecuteReaderAsync();

                if (await npgsqlDr.ReadAsync())
                {
                    userDetail = new UserDetailViewModel();
                    userDetail.Username = username;
                    userDetail.UserId = Convert.ToInt32(npgsqlDr["user_id"]);
                    userDetail.PasswordHash = npgsqlDr["password_hash"]?.ToString() ?? string.Empty;
                    userDetail.PasswordSalt = npgsqlDr["password_salt"]?.ToString() ?? string.Empty;
                    userDetail.IsVerified = Convert.ToBoolean(npgsqlDr["is_verified"]);
                    userDetail.IsActive = Convert.ToBoolean(npgsqlDr["is_active"]);
                    userDetail.IsLocked = Convert.ToBoolean(npgsqlDr["is_locked"]);
                    userDetail.PasswordAttempts = Convert.ToInt32(npgsqlDr["password_atempts"]);
                    userDetail.ChangedInitialPassword = Convert.ToBoolean(npgsqlDr["changed_initial_password"]);
                    userDetail.LockedTime = npgsqlDr.IsDBNull(npgsqlDr.GetOrdinal("locked_time")) ? null : Convert.ToDateTime(npgsqlDr["locked_time"]);
                    userDetail.UserId = Convert.ToInt32(npgsqlDr["user_id"]);
                    userDetail.UserFirstName = npgsqlDr["user_first_name"]?.ToString() ?? string.Empty;
                    userDetail.UserMiddleName = npgsqlDr["user_middle_name"]?.ToString() ?? string.Empty;
                    userDetail.UserLastName = npgsqlDr["user_last_name"]?.ToString() ?? string.Empty;
                    userDetail.UserEmail = npgsqlDr["user_email"]?.ToString() ?? string.Empty;
                    userDetail.Cpf = npgsqlDr["cpf"]?.ToString() ?? string.Empty;
                    userDetail.Rg = npgsqlDr["rg"]?.ToString() ?? string.Empty;
                    userDetail.DateOfBirth = Convert.ToDateTime(npgsqlDr["date_of_birth"]);
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
            return userDetail;
        }
    }
}
