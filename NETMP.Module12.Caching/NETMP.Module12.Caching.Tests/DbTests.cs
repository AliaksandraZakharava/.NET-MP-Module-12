using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETMP.Module12.Caching.DbCaching;
using NETMP.Module12.Caching.NorthwindDb;

namespace NETMP.Module12.Caching.Tests
{
    [TestClass]
    public class DbTests
    {
        private readonly Stopwatch _stopwatch;

        public DbTests()
        {
            _stopwatch = new Stopwatch();
        }

        [TestMethod]
        public void CustomerManager_GetCustomerById_InProcessorCacheTest()
        {
            var cache = new InProcessorCache.Cache<IEnumerable<Customer>>();
            var customerManager = new CustomersManager(cache);

            long executionTimeWithoutCaching = 0;
            long executionTimeWithCaching = 0;

            var id = "BOLID";

            _stopwatch.Start();

            var inProcessorCacheResult1 = customerManager.GetCustomerById(id);

            _stopwatch.Stop();

            executionTimeWithoutCaching = _stopwatch.ElapsedMilliseconds;

            _stopwatch.Restart();

            var inProcessorCacheResult2 = customerManager.GetCustomerById(id);

            _stopwatch.Stop();

            executionTimeWithCaching = _stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(executionTimeWithoutCaching > executionTimeWithCaching);
        }

        [TestMethod]
        public void CustomerManager_GetCustomersByCompany_InProcessorCacheTest()
        {
            var cache = new InProcessorCache.Cache<IEnumerable<Customer>>();
            var customerManager = new CustomersManager(cache);

            long executionTimeWithoutCaching = 0;
            long executionTimeWithCaching = 0;

            var company = "LINO-Delicateses";

            _stopwatch.Start();

            var inProcessorCacheResult1 = customerManager.GetCustomersByCompany(company);

            _stopwatch.Stop();

            executionTimeWithoutCaching = _stopwatch.ElapsedMilliseconds;

            _stopwatch.Restart();

            var inProcessorCacheResult2 = customerManager.GetCustomersByCompany(company);

            _stopwatch.Stop();

            executionTimeWithCaching = _stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(executionTimeWithoutCaching > executionTimeWithCaching);
        }

        [TestMethod]
        public void CustomerManager_GetCustomersByRegion_InProcessorCacheTest()
        {
            var cache = new InProcessorCache.Cache<IEnumerable<Customer>>();
            var customerManager = new CustomersManager(cache);

            long executionTimeWithoutCaching = 0;
            long executionTimeWithCaching = 0;

            var region = "RJ";

            _stopwatch.Start();

            var inProcessorCacheResult1 = customerManager.GetCustomersByRegion(region);

            _stopwatch.Stop();

            executionTimeWithoutCaching = _stopwatch.ElapsedMilliseconds;

            _stopwatch.Restart();

            var inProcessorCacheResult2 = customerManager.GetCustomersByRegion(region);

            _stopwatch.Stop();

            executionTimeWithCaching = _stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(executionTimeWithoutCaching > executionTimeWithCaching);
        }

        [TestMethod]
        public void OrdersManager_GetOrderById_InProcessorCacheTest()
        {
            var cache = new InProcessorCache.Cache<IEnumerable<Order>>();
            var ordersManager = new OrdersManager(cache);

            long executionTimeWithoutCaching = 0;
            long executionTimeWithCaching = 0;

            var id = 10930;

            _stopwatch.Start();

            var inProcessorCacheResult1 = ordersManager.GetOrderById(id);

            _stopwatch.Stop();

            executionTimeWithoutCaching = _stopwatch.ElapsedMilliseconds;

            _stopwatch.Restart();

            var inProcessorCacheResult2 = ordersManager.GetOrderById(id);

            _stopwatch.Stop();

            executionTimeWithCaching = _stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(executionTimeWithoutCaching > executionTimeWithCaching);
        }

        [TestMethod]
        public void CustomerManager_GetOrderByCustomerId_InProcessorCacheTest()
        {
            var cache = new InProcessorCache.Cache<IEnumerable<Order>>();
            var ordersManager = new OrdersManager(cache);

            long executionTimeWithoutCaching = 0;
            long executionTimeWithCaching = 0;

            var customerId = "THEBI";

            _stopwatch.Start();

            var inProcessorCacheResult1 = ordersManager.GetOrderByCustomerId(customerId);

            _stopwatch.Stop();

            executionTimeWithoutCaching = _stopwatch.ElapsedMilliseconds;

            _stopwatch.Restart();

            var inProcessorCacheResult2 = ordersManager.GetOrderByCustomerId(customerId);

            _stopwatch.Stop();

            executionTimeWithCaching = _stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(executionTimeWithoutCaching > executionTimeWithCaching);
        }
    }
}
