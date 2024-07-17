using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
	public class UserLoginRepository : IUserLoginRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;
		private ErrorHelper error;

		public UserLoginRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
			error = new ErrorHelper();
		}

		public async Task<Tuple<RegisterViewModel, ErrorHelper>> CreateAsync(RegisterViewModel register)
		{
			error.HasError = false;
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_user_login_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				//postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
				cmd.Parameters.AddWithValue("p_username", register.Username);
				cmd.Parameters.AddWithValue("p_user_unique_id", register.UserUniqueId);
				cmd.Parameters.AddWithValue("p_password_hash", register.Password);
				cmd.Parameters.AddWithValue("p_password_salt", register.Password);

				cmd.Parameters.AddWithValue("p_error", error.HasError).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.AddWithValue("p_out_user_id", register.UserId).Direction = ParameterDirection.Output;
				cmd.Parameters.AddWithValue("p_out_message", error.ErrorMessage).Direction = ParameterDirection.Output;
				await _connection.OpenAsync();

				cmd.ExecuteNonQuery();

				error.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
				error.ErrorMessage = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
				if (!error.HasError)
				{
					register.UserId = Convert.ToInt32(cmd.Parameters["p_out_user_id"].Value);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				await _connection.CloseAsync();
			}
			return new Tuple<RegisterViewModel, ErrorHelper>(register, error);
		}
	}
}
