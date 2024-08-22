using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;
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
        public ErrorHelper Error;

        public UserAddressRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            Error = new();
        }
        public async Task<Tuple<UserAddress, ErrorHelper>> CreateAsync(UserAddress userAddress)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_address_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_id", userAddress.UserId);
                cmd.Parameters.AddWithValue("p_street_id", userAddress.StreetId);
                cmd.Parameters.AddWithValue("p_complement", userAddress.Complement);
                cmd.Parameters.AddWithValue("p_address_type_id", userAddress.AddressTypeId);
                cmd.Parameters.AddWithValue("p_main_address", userAddress.MainAddress);
                cmd.Parameters.AddWithValue("p_out_address_id", userAddress.AddressId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_created_by", userAddress.CreatedBy);
                cmd.Parameters.AddWithValue("p_modified_by", userAddress.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", Error.HasError).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();
                cmd.ExecuteNonQuery();

                Error.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
                if (!Error.HasError)
                {
                    userAddress.AddressId = (int)(cmd.Parameters["p_out_address_id"].Value ?? -1);
                    if(userAddress.AddressId == -1)
                    {
                        throw new Exception("Address Id can not be negative!");
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrorMessage = ex.Message;
                Error.HasError = true;
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return new Tuple<UserAddress, ErrorHelper>(userAddress, Error);
        }

        public Task<IEnumerable<UserAddress>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<State> ReadByIdAsync(UserAddressViewModel userAddress)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UserAddress userAddress)
        {
            throw new NotImplementedException();
        }

        
    }
}
