using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public interface IUserFactory
    {
        User CreateUser(string role);
    }

}
