using Newtonsoft.Json;
using NETMP.Module12.Caching.Common;
using StackExchange.Redis;

namespace NETMP.Module12.Caching.RedisCache
{
    public class Cache<T> : ICache<T>
    {
        private static IDatabase _cache;

        static Cache()
        {
            _cache = RedisConnectorHelper.Connection.GetDatabase();
        }

        public void AddToCache(string key, object item)
        {
            var json = JsonConvert.SerializeObject(item);

            _cache.StringSet(key, json);
        }

        public void DeleteFromCache(string key)
        {
            _cache.KeyDelete(key);
        }

        public T TryGetFromCache(string key)
        {
            var json = _cache.StringGet(key);

            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
