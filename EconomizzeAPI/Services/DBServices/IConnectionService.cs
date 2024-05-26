using Npgsql;

namespace EconomizzeAPI.Services.DBServices
{
	public interface IConnectionService
	{
		NpgsqlConnection GetConnection();
	}
}
