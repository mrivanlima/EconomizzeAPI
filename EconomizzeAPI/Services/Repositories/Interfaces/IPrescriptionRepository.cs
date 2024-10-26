using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IPrescriptionRepository
	{
		Task<Tuple<Prescription, StatusHelper>> CreatePrescriptionAsync(Prescription prescription);
		Task<Tuple<QuotePrescription, StatusHelper>> CreateQuotePrescriptionAsync(QuotePrescription prescription);
	}
}
