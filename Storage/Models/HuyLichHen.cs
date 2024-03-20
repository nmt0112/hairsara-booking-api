
namespace Storage.Models
{
    public class HuyLichHen
    {
        public int Id { get; set; }
        public string LyDoHuyLich { get; set; }
        public DateTime ThoiGianHuy { get; set; }
        public int IdBooking { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
