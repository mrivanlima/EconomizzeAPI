using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IUserLoginRepository
	{
		Task<Tuple<RegisterViewModel, StatusHelper>> CreateAsync(RegisterViewModel register);

        Task<UserLogin> ReadUserByUserName(UserLoginViewModel login);

        Task<UserLogin> ReadByIdAsync(int id);

        Task<StatusHelper> UserVerifyAsync(int userId, Guid userUniqueId);

        Task<Tuple<LoggedInPasswordViewModel, StatusHelper>> ChangeUserPassword(LoggedInPasswordViewModel login);
    }
}
