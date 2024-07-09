using Economizze.Library;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IProfessionRepository
    {
        Task<Tuple<Profession, bool>> CreateAsync(Profession profession);
        Task<bool> UpdateAsync(Profession profession);
        Task<Profession> ReadByIdAsync(short id);
        Task<IEnumerable<Profession>> ReadAllAsync();
    }
}
