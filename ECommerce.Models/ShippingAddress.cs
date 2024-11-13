using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ShippingAddress
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public string CustomerId { get; set; }

        [ValidateNever]
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required]
        public string FullName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsDefault { get; set; }=false;

       
    }
}
