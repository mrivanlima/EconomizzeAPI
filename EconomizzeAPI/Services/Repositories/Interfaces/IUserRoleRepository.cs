using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IUserRoleRepository
	{
		Task<Tuple<UserRole, bool>> CreateAsync(UserRole userRole);
		Task<bool> UpdateAsync(UserRole userRole);
		Task<UserRole> ReadByIdAsync(short id);
		Task<IEnumerable<Role>> ReadAllAsync(UserRoleViewModel userRole);
	}
}
