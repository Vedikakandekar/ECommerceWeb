using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModels
{
    public class AllProductsVM
    {
        public List<Products> ProductList = new List<Products>();
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}
