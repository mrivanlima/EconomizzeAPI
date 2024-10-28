using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IRoleRepository
    {
		Task<Tuple<Role, StatusHelper>> CreateRoleAsync(Role role);
		Task<IEnumerable<Role>> ReadAllRolesAsync();
	}
}
