using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IUserLoginRepository
	{
        //register new user
		Task<Tuple<RegisterViewModel, StatusHelper>> CreateAsync(RegisterViewModel register);

        //change password
        Task<Tuple<LoggedInPasswordViewModel, StatusHelper>> ChangeUserPassword(LoggedInPasswordViewModel login);
        Task<Tuple<ForgotPasswordViewModel, StatusHelper>> ChangeUserForgotPassword(ForgotPasswordViewModel login);

        //read id and uuid for a user based off of username
        Task<ForgotPasswordViewModel> ReadIdUuid(ForgotPasswordViewModel login);

        //verify a user
        Task<StatusHelper> UserVerifyAsync(int userId, Guid userUniqueId);

        //read UserLogin fields based off of username
        Task<UserLogin> ReadUserByUserName(UserLoginViewModel login);

        //read UserLogin fields based off of ID
        Task<UserLogin> ReadByIdAsync(int id);
    }
}
