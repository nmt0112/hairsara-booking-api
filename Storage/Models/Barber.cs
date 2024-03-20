using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Barber
    {
        public int Id { get; set; }
        public string NameBarber { get; set; }

        public int IdChiNhanhWork { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }

        public string IdUserBarber { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
