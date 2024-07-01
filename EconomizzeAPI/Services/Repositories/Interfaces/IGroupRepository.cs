using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Tuple<Group, bool>> CreateAsync(Group group);
        Task<bool> UpdateAsync(Group group);
        Task<Group> ReadByIdAsync(short id);
        Task<IEnumerable<Group>> ReadAllAsync();
    }
}
