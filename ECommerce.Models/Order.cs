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
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Foreign Key to Customer/User
        [Required]
        public string customerId { get; set; }

        [ValidateNever]
        [ForeignKey("customerId")]
        public virtual Customer customer { get; set; }
        [Required]
        public string AddressId { get; set; }

        // Navigation property to OrderItems
        [ValidateNever]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }

}
