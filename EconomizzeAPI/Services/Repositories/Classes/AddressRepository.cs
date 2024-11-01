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

        public AddressDetail AddressDetail { get; set; }

        #region CONSTRUCTOR
        public AddressRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            AddressDetail = new();
        }
        #endregion

        #region READ ADDRESS DETAILS BY ZIPCODE
        public async Task<AddressDetail> ReadByZipCodeAsync(string ZipCode)
        {
            NpgsqlDataReader? npgsqlDr = null;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.v_street_details WHERE ZIPCODE = @ZipCode", _connection);
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);

            try
            {
                await _connection.OpenAsync();
                npgsqlDr = await cmd.ExecuteReaderAsync();

                if (await npgsqlDr.ReadAsync())
                {

                    AddressDetail.StreetId = Convert.ToInt32(npgsqlDr["street_id"]);
                    AddressDetail.StreetName = npgsqlDr["street_name"].ToString();
                    AddressDetail.StreetNameAscii = npgsqlDr["street_name_ascii"].ToString();
                    AddressDetail.Zipcode = npgsqlDr["zipcode"].ToString();
                    AddressDetail.NeighborhoodName = npgsqlDr["neighborhood_name"].ToString();
                    AddressDetail.CityName = npgsqlDr["city_name"].ToString();
                    AddressDetail.StateName = npgsqlDr["state_name"].ToString();
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
            return AddressDetail;
        }
        #endregion
    }
}
