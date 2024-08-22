using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IUserLoginRepository
	{
		Task<Tuple<RegisterViewModel, ErrorHelper>> CreateAsync(RegisterViewModel register);

        Task<UserLogin> ReadUserByUserName(UserLoginViewModel login);

        Task<ErrorHelper> UserVerifyAsync(int userId, Guid userUniqueId);


    }
}
