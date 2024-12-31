using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using System.Runtime.Caching;
namespace ECommerce.Data.Repository
{
    public class ProductsCacheRepository :ICompanySettingsCacheRepository
    {
        public readonly MemoryCache _cache;
        private static readonly ReaderWriterLockSlim _cacheLock = new();
        public ProductsCacheRepository(MemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public void Loadproducts(List<Products> products)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                foreach (var product in products)
                {
                    var cacheOptions = new CacheItemPolicy()
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(1) // Sliding expiration
                    };
                    _cache.Add(product.Id.ToString(), product, cacheOptions);
                }
                Console.WriteLine("\n\n[x] Info : Data Loaded into Cache \n\n");
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }
        public void FlushCache(List<Products> products)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                foreach (var product in products)
                {
                    _cache.Remove(product.Id.ToString());
                }
                Console.WriteLine("\n\n[x] Info : Data Flushed from cache \n\n");
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }
        public Products? GetFeatureValue(int featureId)
        {
            _cacheLock.EnterReadLock();
            try
            {
                var data = (Products)_cache.Get(featureId.ToString());
                if (data == null)
                {
                    return null;
                }
                Console.WriteLine("\n\n[x] Info : Feature found in cache \n\n");
                return data;
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }
        public void AddFeatureToCache(Products product)
        {
                _cacheLock.EnterWriteLock();
                try
                {
                    if (product.Id != 0)
                {
                    var cacheOptions = new CacheItemPolicy()
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(1), // Sliding expiration

                    };
                    _cache.Add(product.Id.ToString(), product, cacheOptions);
                }
                Console.WriteLine("\n\n[x] Info : Added Feature in cache\n\n");
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public List<Products> GetAllProducts()
        {
            _cacheLock.EnterReadLock();
            try
            {
                List<Products> products = new List<Products>();
                foreach (var item in _cache)
                {
                    products.Add((Products)item.Value);
                }
                return products;
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }
    }
}
