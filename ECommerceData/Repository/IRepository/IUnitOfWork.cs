using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        IProductRepository Product { get; }

        ICartRepository Cart { get; }

        ICartItemRepository CartItem { get; }

        IOrderRepository Order { get; }

        IOrderItemRepository OrderItem { get; }

        IShippingAddressRepository ShippingAddress { get; }

        IOrderItemStatusRepository OrderItemStatus { get; }

        void Save();
    }
}
