using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Models;

namespace ECommerce.Models
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string role)
        {
            return role switch
            {
                StaticDetails.Role_Admin => new Admin(),
                StaticDetails.Role_Customer => new Customer(),
                StaticDetails.Role_Seller => new Seller(),
                _ => throw new ArgumentException("Invalid role type")
            };
        }
    }

}
