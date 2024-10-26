using Economizze.Library;
using EconomizzeAPI.Helper;

namespace EconomizzeAPI.Services.Repositories.Interfaces
{
    public interface IProfessionRepository
    {
        Task<Tuple<Profession, StatusHelper>> CreateProfessionAsync(Profession profession);
        Task<IEnumerable<Profession>> ReadAllProfessionsAsync();
    }
}
