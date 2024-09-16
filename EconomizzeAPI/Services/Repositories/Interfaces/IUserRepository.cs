using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Tuple<User, StatusHelper>> CreateAsync(User user);
        Task<UserDetailViewModel> ReadUserByUsername(string username);
        Task<User> ReadByIdAsync(int id);
    }
}
