using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Tuple<Group, StatusHelper>> CreateGroupAsync(Group group);
        Task<IEnumerable<Group>> ReadAllGroupsAsync();
    }
}
