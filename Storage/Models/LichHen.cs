using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class LichHen
    {
        public int Id { get; set; }
        public bool TrangThaiHoanThanh { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public int IdBooking { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
