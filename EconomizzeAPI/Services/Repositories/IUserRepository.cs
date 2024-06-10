using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories
{
    public interface IUserRepository
    {
        Task<Tuple<UserSetUp, ErrorHelper>> CreateAsync(UserSetUp user);

        Task<UserDetailViewModel> ReadUserByUsername(string username);
        Task<State> ReadByIdAsync(int id);
    }
}
