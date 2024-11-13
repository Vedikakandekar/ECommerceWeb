using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository.IRepository
{
    public interface IShippingAddressRepository : IRepository<ShippingAddress>
    {
        public void Update(ShippingAddress entity);
    }
}
