using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories
{
	public interface IStateRepository
	{
		Task<Tuple<State, bool>> CreateAsync(State state);
		Task<bool> UpdateAsync(State state);
		Task<State> ReadByIdAsync(short id);
		Task<IEnumerable<State>> ReadAllAsync();
	}
}
