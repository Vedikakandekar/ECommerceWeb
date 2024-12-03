namespace ECommerce.Models
{
    public class Customer : User
    {

        public virtual ICollection<Likes>? Likes { get; set; }

    }
}
