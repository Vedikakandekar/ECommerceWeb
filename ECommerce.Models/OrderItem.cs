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
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        // Foreign Key to Order
        [Required]
        public int OrderId { get; set; }

        [ValidateNever]
        
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        // Foreign Key to Product
        [Required]
        public int ProductId { get; set; }

        [ValidateNever]
        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }

        [Required]
        public int  StatusId { get; set; }

        [ValidateNever]
        [ForeignKey("StatusId")]
        public virtual OrderItemStatus Status { get; set; }
    }
}
