namespace NETMP.Module12.Caching.Common
{
    public interface ICache<out T>
    {
        void AddToCache(string key, object item);

        void DeleteFromCache(string key);

        T TryGetFromCache(string key);
    }
}
