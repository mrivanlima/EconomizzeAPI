using Economizze.Library;
using EconomizzeAPI.Services.DBServices;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;

        public UserRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
        }

        public async Task<Tuple<UserSetUp, bool>> CreateAsync(UserSetUp user)
        {

            bool error = false;
            var message = "";
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_setup", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_user_first_name", user.UserFirstName);
                cmd.Parameters.AddWithValue("p_user_email", user.UserEmail);
                cmd.Parameters.AddWithValue("p_username", user.Username);
                cmd.Parameters.AddWithValue("p_password_hash", user.PasswordHash);
                cmd.Parameters.AddWithValue("p_password_salt", user.PasswordSalt);
                cmd.Parameters.AddWithValue("p_user_middle_name", user.UserMiddleName ?? string.Empty);
                cmd.Parameters.AddWithValue("p_user_last_name", user.UserLastName ?? string.Empty);
                cmd.Parameters.AddWithValue("p_cpf", user.CPF ?? string.Empty);
                cmd.Parameters.AddWithValue("p_rg", user.RG ?? string.Empty);
                //cmd.Parameters.AddWithValue("p_date_of_birth", (DateTime)user.DateOfBirth );
                cmd.Parameters.AddWithValue("p_is_verified", user.IsVerified);
                cmd.Parameters.AddWithValue("p_is_active", user.IsActive);
                cmd.Parameters.AddWithValue("p_is_locked", user.IsLocked);
                cmd.Parameters.AddWithValue("p_password_attempts", user.PasswordAttempts);
                cmd.Parameters.AddWithValue("p_changed_initial_password", user.ChangedInitialPassword);
                //cmd.Parameters.AddWithValue("p_locked_time", user.LockedTime);
                //cmd.Parameters.AddWithValue("p_created_by", user.CreatedBy);
                //cmd.Parameters.AddWithValue("p_modified_by", user.ModifiedBy);



                cmd.Parameters.AddWithValue("p_error", error).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.AddWithValue("p_out_user_id", user.UserId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_out_message", message).Direction = ParameterDirection.Output;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

                error = (bool)cmd.Parameters["p_error"].Value;
                message = cmd.Parameters["p_out_message"].Value.ToString();
                if (!error)
                {
                    user.UserId = (int)cmd.Parameters["p_out_user_id"].Value;
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
            return new Tuple<UserSetUp, bool>(user, error);
        }

        public Task<State> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
