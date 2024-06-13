using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<AddressDetail> ReadByZipCodeAsync(string ZipCode);
    }
}
