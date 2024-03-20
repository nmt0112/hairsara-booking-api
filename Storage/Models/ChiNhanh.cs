namespace Storage.Models
{
    public class ChiNhanh
    {
        public int Id { get; set; }
        public string TenChiNhanh { get; set; }
        public string DiaChi { get; set; }

        public int IdViTri { get; set; }
        public virtual ViTri ViTri { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Barber> Barbers { get; set; } = new List<Barber>();
    }
}
