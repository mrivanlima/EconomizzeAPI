using Economizze.Library;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;

namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class AddressTypeRepository : IAddressTypeRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;
        public ICollection<AddressType> AddressTypes { get; set; }
        public AddressType AddressType { get; set; }

        public AddressTypeRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            AddressTypes = new List<AddressType>();
        }
        public Task<Tuple<AddressType, bool>> CreateAsync(AddressType addressType)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddressType>> ReadAllAsync()
        {

            await using var command = new NpgsqlCommand("SELECT * FROM app.usp_api_address_type_read_all()", _connection);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    AddressTypes.Add(new AddressType
                    {
                        AddressTypeId = reader.GetInt16(0),
                        AddressTypeName = reader.GetString(1),
                        AddressTypeNameAscii = reader.GetString(2),
                        CreatedBy = reader.GetInt32(3),
                        CreatedOn = reader.GetDateTime(4),
                        ModifiedBy = reader.GetInt32(5),
                        ModifiedOn = reader.GetDateTime(6)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return AddressTypes;
        }

        public Task<AddressType> ReadByIdAsync(short id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AddressType addressType)
        {
            throw new NotImplementedException();
        }
    }
}
