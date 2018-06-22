using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NETMP.Module12.Caching.Common;
using NETMP.Module12.Caching.NorthwindDb;

namespace NETMP.Module12.Caching.DbCaching
{
    public class OrdersManager
    {
        private const string IdPrefix = "Order_I_";
        private const string CustomerIdPrefix = "Order_C_I_";

        private readonly ICache<IEnumerable<Order>> _cache;
        private readonly Northwind _context;

        public OrdersManager(ICache<IEnumerable<Order>> cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            _context = new Northwind();
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;
        }

        public Order GetOrderById(int id)
        {
            var customers = GetOrCreateDataFromCache(string.Concat(IdPrefix, id), o => o.OrderID == id);

            return customers.FirstOrDefault();
        }

        public IEnumerable<Order> GetOrderByCustomerId(string customerId)
        {
            var customers = GetOrCreateDataFromCache(string.Concat(CustomerIdPrefix, customerId), o => o.CustomerID == customerId);

            return customers;
        }

        #region Private methods

        private IEnumerable<Order> GetOrCreateDataFromCache(string key, Expression<Func<Order, bool>> searchCondition)
        {
            var orders = _cache.TryGetFromCache(key);

            if (orders == null)
            {
                using (_context)
                {
                    orders = _context.Orders.Where(searchCondition);

                    if (!orders.Any())
                    {
                        _cache.AddToCache(key, orders);
                    }
                }
            }

            return orders;
        }

        #endregion
    }
}
