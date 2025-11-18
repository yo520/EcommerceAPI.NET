using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistance.Identity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string DesplayNamr { get; set; } = null!;
        public Address? Address { get; set; } 
    }
}
