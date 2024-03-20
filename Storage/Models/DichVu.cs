

namespace Storage.Models
{
    public class DichVu
    {
        public int Id { get; set; }
        public string TenDichVu { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public int IdDanhMuc { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
