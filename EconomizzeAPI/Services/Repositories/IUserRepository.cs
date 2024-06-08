using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories
{
    public interface IUserRepository
    {
        Task<Tuple<UserSetUp, bool>> CreateAsync(UserSetUp user);
        Task<State> ReadByIdAsync(int id);
    }
}
