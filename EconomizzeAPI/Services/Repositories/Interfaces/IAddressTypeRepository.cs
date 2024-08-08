using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IAddressTypeRepository
    {
        Task<Tuple<AddressType, bool>> CreateAsync(AddressType addressType);
        Task<bool> UpdateAsync(AddressType addressType);
        Task<AddressType> ReadByIdAsync(short id);
        Task<IEnumerable<AddressType>> ReadAllAsync();

        ICollection<AddressType> AddressTypes { get; set; }
        AddressType AddressType { get; set; }
    }
}
