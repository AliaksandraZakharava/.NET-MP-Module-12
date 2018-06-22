using System;
using StackExchange.Redis;

namespace NETMP.Module12.Caching.RedisCache
{
    public class RedisConnectorHelper
    {
        private static Lazy<ConnectionMultiplexer> _lazyConnection;

        static RedisConnectorHelper()
        {
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { "localhost"}
            };

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }
        public static ConnectionMultiplexer Connection => _lazyConnection.Value;
    }
}
