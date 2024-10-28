using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IUserLoginRepository
	{
		Task<Tuple<RegisterViewModel, StatusHelper>> CreateUserLoginAsync(RegisterViewModel register);
        Task<Tuple<LoggedInPasswordViewModel, StatusHelper>> ChangeUserPassword(LoggedInPasswordViewModel login);
        Task<Tuple<ForgotPasswordViewModel, StatusHelper>> ChangeUserForgotPassword(ForgotPasswordViewModel login);
        Task<ForgotPasswordViewModel> ReadIdUuid(ForgotPasswordViewModel login);
        Task<StatusHelper> UserVerifyAsync(int userId, Guid userUniqueId);
        Task<UserLogin> ReadUserLoginByUserName(UserLoginViewModel login);
        Task<UserLogin> ReadByIdAsync(int id);
    }
}
