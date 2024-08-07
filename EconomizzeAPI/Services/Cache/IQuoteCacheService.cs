using Economizze.Library;

namespace EconomizzeAPI.Services.Cache
{
    public interface IQuoteCacheService
    {
        void Set(long key, Quote value);
        Quote Get(long key);
        bool Remove(long key);
    }
}
