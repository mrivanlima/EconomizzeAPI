using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;


namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;
        public StatusHelper status;

        #region CONSTRUCTOR
        public UserAddressRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            status = new();
        }
        #endregion

        #region CREATE USER ADDRESS IN DB
        public async Task<Tuple<UserAddress, StatusHelper>> CreateUserAddressAsync(UserAddress userAddress)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_address_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_id", userAddress.UserId);
                cmd.Parameters.AddWithValue("p_street_id", userAddress.StreetId);
                cmd.Parameters.AddWithValue("p_complement", userAddress.Complement);
                cmd.Parameters.AddWithValue("p_main_address", userAddress.MainAddress);
                cmd.Parameters.AddWithValue("p_out_address_id", userAddress.AddressId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_created_by", userAddress.CreatedBy);
                cmd.Parameters.AddWithValue("p_modified_by", userAddress.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
                await _connection.OpenAsync();
                cmd.ExecuteNonQuery();

                status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
                if (!status.HasError)
                {
                    userAddress.AddressId = (int)(cmd.Parameters["p_out_address_id"].Value ?? -1);
                }
                else
                {
                    status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
                status.HasError = true;
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return new Tuple<UserAddress, StatusHelper>(userAddress, status);
        }
        #endregion
    }
}
