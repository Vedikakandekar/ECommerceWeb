using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Likes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }


        [ForeignKey("CustomerId")]
        public Customer? customer { get; set; }


        [Required]
        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public Products? product { get; set; }
    }
}
