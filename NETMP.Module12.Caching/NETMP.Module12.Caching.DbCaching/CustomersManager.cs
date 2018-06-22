using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NETMP.Module12.Caching.Common;
using NETMP.Module12.Caching.NorthwindDb;

namespace NETMP.Module12.Caching.DbCaching
{
    public class CustomersManager
    {
        private const string IdPrefix = "Customer_I_";
        private const string CompanyPrefix = "Customer_C_";
        private const string RegionPrefix = "Customer_R_";

        private readonly ICache<IEnumerable<Customer>> _cache;
        private readonly Northwind _context;

        public CustomersManager(ICache<IEnumerable<Customer>> cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            _context = new Northwind();
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;
        }

        public Customer GetCustomerById(string id)
        {
            var customers = GetOrCreateDataFromCache(string.Concat(IdPrefix, id), c => c.CustomerID == id);

            return customers.FirstOrDefault();
        }

        public IEnumerable<Customer> GetCustomersByCompany(string companyName)
        {
            var customer = GetOrCreateDataFromCache(string.Concat(CompanyPrefix, companyName), c => c.CompanyName == companyName);

            return customer;
        }

        public IEnumerable<Customer> GetCustomersByRegion(string region)
        {
            var customer = GetOrCreateDataFromCache(string.Concat(RegionPrefix, region), c => c.Region == region);

            return customer;
        }

        #region Private methods

        private IEnumerable<Customer> GetOrCreateDataFromCache(string key, Expression<Func<Customer, bool>> searchCondition)
        {
            var customers = _cache.TryGetFromCache(key);

            if (customers == null)
            {
                using (_context)
                {
                    customers = _context.Customers.Where(searchCondition);

                    if (!customers.Any())
                    {
                        _cache.AddToCache(key, customers);
                    }
                }
            }

            return customers;
        }

        #endregion
    }
}
