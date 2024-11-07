using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModels
{
    public class CartVM
    {
        public CartVM()
        {
            cart = new Cart();
            item = new CartItem();
        }

        public Cart cart { get; set; }

        public CartItem item { get; set; }

        public float subtotal { get; set; }

        public float total { get; set; }

        public float shippingFees { get; set; } 
    }
}
