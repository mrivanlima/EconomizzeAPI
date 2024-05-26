using Economizze.Library;
using EconomizzeAPI.Services.DBServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace EconomizzeAPI.Services.Repositories
{
	public class StateRepository : IStateRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;

		public StateRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
		}
		public async Task<Tuple<State, bool>> CreateAsync(State state)
		{
	
			bool error = false;
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_state_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("p_out_state_id", state.StateId).Direction = ParameterDirection.Output;
				cmd.Parameters.AddWithValue("p_state_name", state.StateName);
				cmd.Parameters.AddWithValue("p_longitude", state.Longitude.HasValue ? (object)state.Longitude.Value : DBNull.Value);
				cmd.Parameters.AddWithValue("p_latitude", state.Latitude.HasValue ? (object)state.Latitude.Value : DBNull.Value);
				cmd.Parameters.AddWithValue("p_created_by", state.CreatedBy);
				cmd.Parameters.AddWithValue("p_modified_by", state.ModifiedBy);
				cmd.Parameters.AddWithValue("p_error", error).Direction = ParameterDirection.InputOutput;
				_connection.Open(); 

				cmd.ExecuteNonQuery();

				error = (bool)cmd.Parameters["p_error"].Value;
				if (!error)
				{
					state.StateId = (short)cmd.Parameters["p_out_state_id"].Value;
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
			return new Tuple<State, bool>(state, error);
		}
	}
}
