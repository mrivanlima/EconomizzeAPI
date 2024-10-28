using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
	public interface IQuoteRepository
	{
		Task<Tuple<Quote, StatusHelper>> CreateQuoteAsync(Quote quote);
		Task<Neighborhood> FindNeighborhoodId(int streetId);
	}
}
