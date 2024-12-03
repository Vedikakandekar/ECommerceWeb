using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModels
{
    public class ProductVM
    {
        public Products Products { get; set; }

      
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

      
    }
}
