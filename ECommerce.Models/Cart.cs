using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ECommerce.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [ValidateNever]
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }  

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;  

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
