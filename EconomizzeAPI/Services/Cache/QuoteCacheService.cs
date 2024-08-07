using Economizze.Library;

namespace EconomizzeAPI.Services.Cache
{
    public class QuoteCacheService : IQuoteCacheService
    {
        private readonly Dictionary<long, Quote> _quoteCache;
        private readonly object _lock = new object();

        public QuoteCacheService()
        {
            _quoteCache = new();
        }

        public void Set(long key, Quote value)
        {
            lock (_lock)
            {
                _quoteCache[key] = value;
            }
        }

        public Quote Get(long key)
        {
            lock (_lock)
            {
                if (_quoteCache.TryGetValue(key, out var value))
                {
                    return value;
                }
                return null;
            }
        }

        public bool Remove(long key)
        {
            lock (_lock)
            {
                return _quoteCache.Remove(key);
            }
        }
    }
}
