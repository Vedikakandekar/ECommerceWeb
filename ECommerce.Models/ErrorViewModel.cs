using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class ErrorViewModel
    {
        [Key]
        public string? RequestId { get; set; } = "0";

        public string Name { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
