using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IRoleRepository
    {
		Task<Tuple<Role, bool>> CreateAsync(Role role);
		Task<bool> UpdateAsync(Role role);
		Task<Role> ReadByIdAsync(short id);
		Task<IEnumerable<Role>> ReadAllAsync();
	}
}
