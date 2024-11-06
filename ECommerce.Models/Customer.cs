namespace ECommerce.Models
{
    public class Customer : ApplicationUser
    {

        public List<string>? Notifications { get; set; }
 
    }
}
