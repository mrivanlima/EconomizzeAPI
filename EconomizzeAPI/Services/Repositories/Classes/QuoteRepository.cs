using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
	public class QuoteRepository : IQuoteRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;

		private StatusHelper status;

		public Neighborhood neighborhood { get; set; }

		#region CONSTRUCTOR
		public QuoteRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
			status = new StatusHelper();
			neighborhood = new();
		}
		#endregion

		#region CREATE QUOTE
		public async Task<Tuple<Quote, StatusHelper>> CreateQuoteAsync(Quote quote)
		{
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_quote_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				//postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
				cmd.Parameters.AddWithValue("p_user_id", quote.UserId);
				cmd.Parameters.AddWithValue("p_neighborhood_id", quote.NeighborhoodId);
				cmd.Parameters.AddWithValue("p_created_by", quote.UserId);
				cmd.Parameters.AddWithValue("p_modified_by", quote.UserId);

				cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.AddWithValue("p_out_quote_id", quote.QuoteId).Direction = ParameterDirection.Output;
				cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
				await _connection.OpenAsync();

				//cmd.ExecuteNonQuery();
				await cmd.ExecuteReaderAsync();

				status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
				status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
				if (!status.HasError)
				{
					quote.QuoteId = Convert.ToInt32(cmd.Parameters["p_out_quote_id"].Value);
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
			return new Tuple<Quote, StatusHelper>(quote, status);
		}
		#endregion

		#region FIND NEIGHBORHOOD ID
		public async Task<Neighborhood> FindNeighborhoodId(int streetId)
		{
			NpgsqlDataReader? npgsqlDr = null;
			NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.v_street_details WHERE STREET_ID = @street_id", _connection);
			cmd.Parameters.AddWithValue("@street_id", streetId);

			try
			{
				await _connection.OpenAsync();
				npgsqlDr = await cmd.ExecuteReaderAsync();

				if (await npgsqlDr.ReadAsync())
				{
					neighborhood.NeighborhoodId = npgsqlDr.GetInt32(npgsqlDr.GetOrdinal("neighborhood_id"));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				await _connection.CloseAsync();
				if (npgsqlDr != null)
				{
					await npgsqlDr.CloseAsync();
				}
			}
			return neighborhood;
		}
		#endregion
	}
}
