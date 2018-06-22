using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETMP.Module12.Caching.Fibonacci;
using InProcessor = NETMP.Module12.Caching.InProcessorCache;
using Redis = NETMP.Module12.Caching.RedisCache;

namespace NETMP.Module12.Caching.Tests
{
    [TestClass]
    public class FibonacciTests
    {
        private readonly Stopwatch _stopwatch;

        public FibonacciTests()
        {
            _stopwatch = new Stopwatch();
        }

        [TestMethod]
        public void GetFibonacciNumbers_InProcessorCacheTest()
        {
            var cache = new InProcessor.Cache<IEnumerable<int>>();
            var numberSequenceProvider = new NumberSequenceProvider(cache);

            long executionTimeWithoutCaching = 0;
            long executionTimeWithCaching = 0;

            _stopwatch.Start();

            var inProcessorCacheResult1 = numberSequenceProvider.GetFibonacciNumbers(10000);

            _stopwatch.Stop();

            executionTimeWithoutCaching = _stopwatch.ElapsedMilliseconds;

            _stopwatch.Restart();

            var inProcessorCacheResult2 = numberSequenceProvider.GetFibonacciNumbers(10000);

            _stopwatch.Stop();

            executionTimeWithCaching = _stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(executionTimeWithoutCaching > executionTimeWithCaching);
        }

        [TestMethod]
        public void GetFibonacciNumbers_RedisCacheTest()
        {
            var cache = new Redis.Cache<IEnumerable<int>>();
            var numberSequenceProvider = new NumberSequenceProvider(cache);

            long executionTimeWithoutCaching = 0;
            long executionTimeWithCaching = 0;

            _stopwatch.Start();

            var inProcessorCacheResult1 = numberSequenceProvider.GetFibonacciNumbers(10000).ToList();

            _stopwatch.Stop();

            executionTimeWithoutCaching = _stopwatch.ElapsedMilliseconds;

            _stopwatch.Restart();

            var inProcessorCacheResult2 = numberSequenceProvider.GetFibonacciNumbers(10000).ToList();

            _stopwatch.Stop();

            executionTimeWithCaching = _stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(executionTimeWithoutCaching > executionTimeWithCaching);
        }
    }
}
