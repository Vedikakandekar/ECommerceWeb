using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModels
{
   public class OrderVM
    {
        public List<OrderItem> orderItemsList { get; set; }

        public Dictionary<int, ShippingAddress> OrderShippingAddresses { get; set; } = new Dictionary<int, ShippingAddress>();
        public IEnumerable<SelectListItem>? StatusList { get; set; }

    }
}
