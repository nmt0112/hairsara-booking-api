
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public bool TrangThai { get; set; }

        public int IdCustomer { get; set; }
        public virtual Customer Customer { get; set; }

        public int IdBarber { get; set; }
        public virtual Barber Barber { get; set; }

        public int IdDichVu { get; set; }
        public virtual DichVu DichVu { get; set; }

        public int IdChiNhanh { get; set; }     
        public virtual ChiNhanh ChiNhanh { get; set; }


        public virtual LichHen? LichHen { get; set; }
        public virtual HuyLichHen? HuyLichHen { get; set; }
        [NotMapped]
        public string NameCustomer { get; set; }
    }
}
