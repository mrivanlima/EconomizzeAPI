using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Tuple<User, StatusHelper>> CreateUserAsync(User user);
        Task<UserDetailViewModel> ReadUserByUsername(string username);
        Task<User> ReadUserByIdAsync(int id);
    }
}
