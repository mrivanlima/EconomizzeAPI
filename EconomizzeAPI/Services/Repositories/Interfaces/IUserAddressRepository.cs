using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IUserAddressRepository
    {
        Task<Tuple<UserAddress, StatusHelper>> CreateUserAddressAsync(UserAddress userAddress);
    }
}
