using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class AspNetUsers:IdentityUser
    {
        public virtual Customer Customer { get; set; }
        public virtual Barber Barber { get; set; }
    }
}
