using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
    public class StateRepository : IStateRepository
    {
        private readonly IConnectionService _connect;
        private readonly NpgsqlConnection _connection;

        private StatusHelper status;

        #region CONSTRUCTOR
        public StateRepository(IConnectionService connect)
        {
            _connect = connect;
            _connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
            status = new StatusHelper();
        }
        #endregion

        #region CREATE STATE IN DB
        public async Task<Tuple<State, StatusHelper>> CreateStateAsync(State state)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_state_create", _connection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_out_state_id", state.StateId).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("p_state_name", state.StateName);
                cmd.Parameters.AddWithValue("p_state_uf", state.StateUf);
                cmd.Parameters.AddWithValue("p_longitude", state.Longitude.HasValue ? state.Longitude.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("p_latitude", state.Latitude.HasValue ? state.Latitude.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("p_created_by", state.CreatedBy);
                cmd.Parameters.AddWithValue("p_modified_by", state.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

                status.HasError = (bool)cmd.Parameters["p_error"].Value;
                if (!status.HasError)
                {
                    state.StateId = (short)cmd.Parameters["p_out_state_id"].Value;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                status.HasError = true;
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return new Tuple<State, StatusHelper>(state, status);
        }
        #endregion

        #region READ STATE BY STATE ID
        public async Task<State> ReadStateByIdAsync(short id)
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
                    state.StateName = npgsqlDr.GetString(npgsqlDr.GetOrdinal("state_name"));
                    state.StateUf = npgsqlDr.GetString(npgsqlDr.GetOrdinal("state_uf"));
                    state.Longitude = npgsqlDr.GetDouble(npgsqlDr.GetOrdinal("longitude"));
                    state.Latitude = npgsqlDr.GetDouble(npgsqlDr.GetOrdinal("latitude"));
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
        #endregion

        #region READ ALL STATES IN DB
        public async Task<IEnumerable<State>> ReadAllStatesAsync()
        {
            ICollection<State> states = new List<State>();
            await using var command = new NpgsqlCommand("SELECT * FROM app.usp_api_state_read_all()", _connection);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    states.Add(new State
                    {
                        StateId = reader.GetInt16(0),
                        StateName = reader.GetString(1),
                        Longitude = reader.GetDouble(2),
                        Latitude = reader.GetDouble(3),
                        StateUf = reader.GetString(4)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"status: {ex.Message}");
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return states;
        }
        #endregion

        #region UPDATE STATE IN DB
        public async Task<bool> UpdateStateAsync(State state)
        {
            bool error = false;
            NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_state_update_by_id", _connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_state_id", state.StateId);
                cmd.Parameters.AddWithValue("p_state_name", state.StateName);
                cmd.Parameters.AddWithValue("p_state_name_ascii", state.StateNameAscii);
                cmd.Parameters.AddWithValue("p_state_uf", state.StateUf);
                cmd.Parameters.AddWithValue("p_longitude", state.Longitude.HasValue ? state.Longitude.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("p_latitude", state.Latitude.HasValue ? state.Latitude.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("p_modified_by", state.ModifiedBy);
                cmd.Parameters.AddWithValue("p_error", error).Direction = ParameterDirection.InputOutput;
                await _connection.OpenAsync();

                cmd.ExecuteNonQuery();

#pragma warning disable CS8605 // Unboxing a possibly null value.
                error = (bool)cmd.Parameters["p_error"].Value;
#pragma warning restore CS8605 // Unboxing a possibly null value.


            }
            catch (Exception ex)
            {
                error = true;
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return error;
        }
        #endregion
    }
}
