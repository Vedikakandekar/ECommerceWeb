using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public abstract class User : IdentityUser
    {

        [Required]
        public string Name
        {
            get;
            set;
        }
        
        [Required]
        public string Role
        {
            get;
            set;
        }

    }
}
