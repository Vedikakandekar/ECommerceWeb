using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class ErrorViewModel
    {
        [Key]
        public string? RequestId { get; set; }

        public string Name { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
