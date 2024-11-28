using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModels
{
    public class DetailsDTO
    {
        public Products Product { get; set; }

        public bool IsInCart {  get; set; }
    }
}
