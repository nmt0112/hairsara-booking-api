using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string NameCustomer { get; set; }

        public string IdUserCustomer { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
