﻿using Economizze.Library;
using EconomizzeAPI.Services.DBServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using System.Data;
using System.Data.SqlTypes;
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
				cmd.Parameters.AddWithValue("p_state_uf", state.StateUf);
				cmd.Parameters.AddWithValue("p_longitude", state.Longitude.HasValue ? (object)state.Longitude.Value : DBNull.Value);
				cmd.Parameters.AddWithValue("p_latitude", state.Latitude.HasValue ? (object)state.Latitude.Value : DBNull.Value);
				cmd.Parameters.AddWithValue("p_created_by", state.CreatedBy);
				cmd.Parameters.AddWithValue("p_modified_by", state.ModifiedBy);
				cmd.Parameters.AddWithValue("p_error", error).Direction = ParameterDirection.InputOutput;
				await _connection.OpenAsync();

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

		public async Task<State> ReadByIdAsync(short id)
		{
			NpgsqlDataReader? npgsqlDr = null;
			NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM app.usp_api_state_read_by_id(@p_state_id)", _connection);
			State state = new State();
			cmd.Parameters.AddWithValue("@p_state_id", id);

			try
			{
				await _connection.OpenAsync();
				npgsqlDr = await cmd.ExecuteReaderAsync();

				if (await npgsqlDr.ReadAsync())
				{
					state.StateId = id;
					state.StateName = npgsqlDr.GetString(npgsqlDr.GetOrdinal("out_state_name"));
					state.StateUf = npgsqlDr.GetString(npgsqlDr.GetOrdinal("out_state_uf"));
					state.Longitude = npgsqlDr.GetDouble(npgsqlDr.GetOrdinal("out_longitude"));
					state.Latitude = npgsqlDr.GetDouble(npgsqlDr.GetOrdinal("out_latitude"));
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
			return state;

		}
	}
}