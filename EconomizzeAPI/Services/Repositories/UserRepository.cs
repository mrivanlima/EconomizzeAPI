using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Services.DBServices;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories
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

        public async Task<Tuple<UserSetUp, ErrorHelper>> CreateAsync(UserSetUp user)
        {

            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_setup", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("p_date_of_birth", user.DateOfBirth.HasValue ? user.DateOfBirth.Value : DBNull.Value);
                //postgres picks up timestamp, for taht reason datatypoe date in postgres must be done this way.
                cmd.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date; 
                cmd.Parameters.AddWithValue("p_user_first_name", user.UserFirstName);
                cmd.Parameters.AddWithValue("p_user_email", user.UserEmail);
                cmd.Parameters.AddWithValue("p_username", user.Username);
                cmd.Parameters.AddWithValue("p_password_hash", user.PasswordHash);
                cmd.Parameters.AddWithValue("p_password_salt", user.PasswordSalt);
                cmd.Parameters.AddWithValue("p_user_middle_name", user.UserMiddleName ?? string.Empty);
                cmd.Parameters.AddWithValue("p_user_last_name", user.UserLastName ?? string.Empty);
                cmd.Parameters.AddWithValue("p_cpf", user.CPF ?? string.Empty);
                cmd.Parameters.AddWithValue("p_rg", user.RG ?? string.Empty);
                cmd.Parameters.AddWithValue("p_is_verified", user.IsVerified);
                cmd.Parameters.AddWithValue("p_is_active", user.IsActive);
                cmd.Parameters.AddWithValue("p_is_locked", user.IsLocked);
                cmd.Parameters.AddWithValue("p_password_attempts", user.PasswordAttempts);
                cmd.Parameters.AddWithValue("p_changed_initial_password", user.ChangedInitialPassword);
                cmd.Parameters.AddWithValue("p_locked_time", user.LockedTime.HasValue ? (object)user.LockedTime.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("p_created_by", user.CreatedBy.HasValue ? (object)user.CreatedBy.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("p_modified_by", user.ModifiedBy.HasValue ? (object)user.ModifiedBy.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("p_error", error.HasError).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.AddWithValue("p_out_user_id", user.UserId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_out_message", error.ErrorMessage).Direction = ParameterDirection.Output;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

                error.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
                error.ErrorMessage = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
                if (!error.HasError)
                {
                    user.UserId = Convert.ToInt32(cmd.Parameters["p_out_user_id"].Value);
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
            return new Tuple<UserSetUp, ErrorHelper>(user, error);
        }

        public Task<State> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
