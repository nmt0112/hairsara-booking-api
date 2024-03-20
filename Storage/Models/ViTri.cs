

namespace Storage.Models
{
    public class ViTri
    {
        public int Id { get; set; }
        public string TinhThanhPho { get; set; }
        public virtual ICollection<ChiNhanh> ChiNhanhs { get; set; } = new List<ChiNhanh>();
    }
}
