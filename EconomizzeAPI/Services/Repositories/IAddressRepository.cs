using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories
{
    public interface IAddressRepository
    {
        Task<AddressDetail> ReadByZipCodeAsync(string ZipCode);
    }
}
