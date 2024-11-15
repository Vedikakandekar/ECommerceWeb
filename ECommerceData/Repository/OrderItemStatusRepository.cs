using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository
{
    internal class OrderItemStatusRepository : Repository<OrderItemStatus>, IOrderItemStatusRepository
    {
        private ApplicationDbContext _db;
        public OrderItemStatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(OrderItemStatus entity)
        {
            _db.OrderItemStatus.Update(entity);
        }
    }
}
