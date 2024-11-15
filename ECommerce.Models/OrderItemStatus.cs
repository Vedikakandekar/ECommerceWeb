using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class OrderItemStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }


}
