using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
        //brandId
        //categoryId
        public int CategoryId { get; set; }

        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int stock {  get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        public string SellerId { get; set; }

        [Required]
        [ValidateNever]
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

    }
}
