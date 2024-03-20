

namespace Storage.Models
{
    public class DanhMuc
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTaDanhMuc { get; set; }
        public virtual ICollection<DichVu> DichVus { get; set; } = new List<DichVu>();
    }
}
