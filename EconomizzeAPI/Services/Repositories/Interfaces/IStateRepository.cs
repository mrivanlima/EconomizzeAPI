using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IStateRepository
    {
        Task<Tuple<State, StatusHelper>> CreateStateAsync(State state);
        Task<bool> UpdateStateAsync(State state);
        Task<State> ReadStateByIdAsync(short id);
        Task<IEnumerable<State>> ReadAllStatesAsync();
    }
}
