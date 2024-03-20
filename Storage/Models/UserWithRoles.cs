using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class UserWithRoles
    {
        public Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }
        public List<string> Roles { get; set; }
    }

}
