using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories
{
    public interface IUserRepository
    {
        Task<Tuple<UserSetUp, ErrorHelper>> CreateAsync(UserSetUp user);
        Task<State> ReadByIdAsync(int id);
    }
}
