using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IUserRoleRepository
	{
		Task<Tuple<UserRole, StatusHelper>> CreateUserRoleAsync(UserRole userRole);
		Task<IEnumerable<Role>> ReadAllUserRolesAsync(UserRoleViewModel userRole);
	}
}
