using Npgsql;

namespace EconomizzeAPI.Services.DBServices
{
	public class ConnectionService : IConnectionService
	{
		public NpgsqlConnection connection { get; }
		public ConnectionService()
		{
			var configuration = GetConfiguration();
			connection = new NpgsqlConnection(configuration.GetSection("ConnectionStrings").GetSection("PostgreSql").Value);

		}

		private IConfigurationRoot GetConfiguration()
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			return builder.Build();
		}
		
		public NpgsqlConnection GetConnection()
		{
			return connection;
		}
	}
}
