using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
            shippingAddress = new ShippingAddress();
        }

        public Cart cart { get; set; }

        public CartItem item { get; set; }

        public float subtotal { get; set; }

        public float total { get; set; }

        public float shippingFees { get; set; } 

        public ShippingAddress shippingAddress { get; set; }

        [ValidateNever]
        public List<ShippingAddress> addressList { get; set; } = new List<ShippingAddress>();
    }
}
