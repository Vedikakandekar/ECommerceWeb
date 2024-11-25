using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Models;

namespace ECommerce.Utility
{
   public class ControllerHelper
    {
        public static List<Products> FilterProductList(List<Products> ProductList,string searchString, string categoryFilter,string priceFilter)
        {
            List<Products> FilteredProducts;
            if (ProductList == null || ProductList.Count == 0)
            {
                return new List<Products>();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                FilteredProducts = ProductList.Where(product => product.Name != null && product.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                FilteredProducts = ProductList;
            }
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                FilteredProducts = FilteredProducts.Where(u => u.Category.Name.Contains(categoryFilter)).ToList();
            }
            if (priceFilter == "low-to-high")
            {
                FilteredProducts = FilteredProducts.OrderBy(u => u.Price).ToList();
            }
            if (priceFilter == "high-to-low")
            {
                FilteredProducts = FilteredProducts.OrderByDescending(u => u.Price).ToList();
            }
            return FilteredProducts;
        }
    }
}
