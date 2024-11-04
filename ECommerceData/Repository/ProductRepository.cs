using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository
{  
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Products newProduct)
        {
            var objFromDb = _db.Product.FirstOrDefault(u => u.Id == newProduct.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = newProduct.Name;
                objFromDb.Price = newProduct.Price;
                objFromDb.Description = newProduct.Description;
                objFromDb.CategoryId = newProduct.CategoryId;
                if (newProduct.ImageUrl != null)
                {
                    objFromDb.ImageUrl = newProduct.ImageUrl;
                }
            }
        }
    }
}
