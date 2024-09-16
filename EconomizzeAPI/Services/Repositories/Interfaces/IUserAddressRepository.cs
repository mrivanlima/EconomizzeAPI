using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IUserAddressRepository
    {
        Task<Tuple<UserAddress, StatusHelper>> CreateAsync(UserAddress userAddress);
        Task<bool> UpdateAsync(UserAddress userAddress);
        Task<State> ReadByIdAsync(UserAddressViewModel userAddress);
        Task<IEnumerable<UserAddress>> ReadAllAsync();
    }
}
