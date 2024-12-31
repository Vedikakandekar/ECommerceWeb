using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Services
{
    public class CacheLoaderService : IHostedService
    {
        private readonly ICompanySettingsCacheRepository _cacheRepo;


        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CacheLoaderService(ICompanySettingsCacheRepository companySettingsCache, IServiceScopeFactory serviceScopeFactory)
        {
            _cacheRepo = companySettingsCache;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                List<Products> features = scope.ServiceProvider.GetRequiredService<IUnitOfWork>().Product.GetAll().ToList();
                _cacheRepo.Loadproducts(features);
                Console.WriteLine("[x] Info : Features loaded at application start.");
                return Task.CompletedTask;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
