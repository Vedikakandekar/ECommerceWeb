using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModels
{
    public class ConfirmOrderDTO
    {
        public Order PlacedOrder { get; set; }
        public List<OrderItem> OrderItem  { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
    }
}
