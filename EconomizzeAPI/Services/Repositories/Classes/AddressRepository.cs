using Economizze.Library;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;

namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;

        public AddressRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
        }
        public async Task<AddressDetail> ReadByZipCodeAsync(string ZipCode)
        {
            NpgsqlDataReader? npgsqlDr = null;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.v_street_details WHERE ZIPCODE = @ZipCode", _connection);
            AddressDetail addressDetail = new AddressDetail();
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);

            try
            {
                await _connection.OpenAsync();
                npgsqlDr = await cmd.ExecuteReaderAsync();

                if (await npgsqlDr.ReadAsync())
                {
                    addressDetail.StreetId = Convert.ToInt32(npgsqlDr["street_id"]);
                    addressDetail.StreetName = npgsqlDr["street_name"].ToString();
                    addressDetail.StreetNameAscii = npgsqlDr["street_name_ascii"].ToString();
                    addressDetail.Zipcode = npgsqlDr["zipcode"].ToString();
                    addressDetail.NeighborhoodName = npgsqlDr["neighborhood_name"].ToString();
                    addressDetail.CityName = npgsqlDr["city_name"].ToString();
                    addressDetail.StateName = npgsqlDr["state_name"].ToString();
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
            return addressDetail;
        }
    }
}
