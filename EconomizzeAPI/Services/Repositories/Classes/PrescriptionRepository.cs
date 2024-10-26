using Economizze.Library;
using EconomizzeAPI.Helper;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace EconomizzeAPI.Services.Repositories.Classes
{
	public class PrescriptionRepository : IPrescriptionRepository
	{
		private readonly IConnectionService _connect;
		private readonly NpgsqlConnection _connection;

		private StatusHelper status;

		#region CONSTRUCTOR
		public PrescriptionRepository(IConnectionService connect)
		{
			_connect = connect;
			_connection = connect.GetConnection() ?? throw new ArgumentNullException(nameof(_connect));
			status = new StatusHelper();
		}
		#endregion

		#region CREATE PRESCRIPTION
		public async Task<Tuple<Prescription, StatusHelper>> CreatePrescriptionAsync(Prescription prescription)
		{
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_prescription_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				//postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
				cmd.Parameters.AddWithValue("p_prescription_unique", prescription.PrescriptionUnique);
				cmd.Parameters.AddWithValue("p_prescription_url", prescription.PrescriptionUrl);
				cmd.Parameters.AddWithValue("p_created_by", prescription.CreatedBy);
				cmd.Parameters.AddWithValue("p_modified_by", prescription.ModifiedBy);
				cmd.Parameters.AddWithValue("p_facility_id", prescription.FacilityId);
				cmd.Parameters.AddWithValue("p_professional_id", prescription.ProfessionalId);

				cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.AddWithValue("p_out_prescription_id", prescription.PrescriptionId).Direction = ParameterDirection.Output;
				//cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
				await _connection.OpenAsync();

				//cmd.ExecuteNonQuery();
				await cmd.ExecuteReaderAsync();

				status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
				//status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
				if (!status.HasError)
				{
					prescription.PrescriptionId = Convert.ToInt32(cmd.Parameters["p_out_prescription_id"].Value);
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
			return new Tuple<Prescription, StatusHelper>(prescription, status);
		}
		#endregion

		#region CREATE QUOTE PRESCRIPTION
		public async Task<Tuple<QuotePrescription, StatusHelper>> CreateQuotePrescriptionAsync(QuotePrescription quotePrescription)
		{
			NpgsqlCommand cmd = new NpgsqlCommand("app.usp_api_quote_prescription_create", _connection);

			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				//postgres picks up timestamp, for that reason datatype date in postgres must be done this way.
				cmd.Parameters.AddWithValue("p_quote_id", quotePrescription.QuoteId);
				cmd.Parameters.AddWithValue("p_prescription_id", quotePrescription.PrescriptionId);
				cmd.Parameters.AddWithValue("p_created_by", quotePrescription.CreatedBy);
				cmd.Parameters.AddWithValue("p_modified_by", quotePrescription.ModifiedBy);

				cmd.Parameters.AddWithValue("p_error", status.HasError).Direction = ParameterDirection.InputOutput;
				//cmd.Parameters.AddWithValue("p_out_message", status.Message).Direction = ParameterDirection.Output;
				await _connection.OpenAsync();

				//cmd.ExecuteNonQuery();
				await cmd.ExecuteReaderAsync();

				status.HasError = (bool)(cmd.Parameters["p_error"].Value ?? false);
				//status.Message = cmd.Parameters["p_out_message"].Value?.ToString() ?? "";
				if (!status.HasError)
				{
					quotePrescription.PrescriptionId = Convert.ToInt32(cmd.Parameters["p_prescription_id"].Value);
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
			return new Tuple<QuotePrescription, StatusHelper>(quotePrescription, status);
		}
		#endregion
	}
}
