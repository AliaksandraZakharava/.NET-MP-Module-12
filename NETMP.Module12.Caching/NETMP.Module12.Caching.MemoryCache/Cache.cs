using System.Runtime.Caching;
using NETMP.Module12.Caching.Common;

namespace NETMP.Module12.Caching.InProcessorCache
{
    public class Cache<T> : ICache<T>
    {
        private static object _padlock;
        private static MemoryCache _cache;

        static Cache()
        {
            _cache = new MemoryCache("InProcessorCache");
            _padlock = new object();
        }

        public void AddToCache(string key, object item)
        {
            lock (_padlock)
            {
                var cacheItem = new CacheItem(key, item);

                _cache.Add(cacheItem, new CacheItemPolicy());
            }
        }

        public void DeleteFromCache(string key)
        {
            lock (_padlock)
            {
                _cache.Remove(key);
            }
        }

        public T TryGetFromCache(string key)
        {
            lock (_padlock)
            {
                var result = (T)_cache[key];

                return result;
            }
        }
    }
}
