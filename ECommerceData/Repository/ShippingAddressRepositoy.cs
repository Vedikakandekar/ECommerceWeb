using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository
{
    public class ShippingAddressRepositoy : Repository<ShippingAddress>, IShippingAddressRepository
    {
        private ApplicationDbContext _db;
        public ShippingAddressRepositoy(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ShippingAddress entity)
        {
            _db.ShippingAddress.Update(entity);
        }
    }
}
