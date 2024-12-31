using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository.IRepository
{
    public interface ICompanySettingsCacheRepository
    {

        void Loadproducts(List<Products> products);



        void FlushCache(List<Products> products);


        Products? GetFeatureValue(int featureId);

        void AddFeatureToCache(Products product);
        List<Products> GetAllProducts();


    }
}
