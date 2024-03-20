using System.Text.Json.Serialization;

namespace Storage.Models.DTO
{
    public class DichVuDto
    {
        public string TenDichVu { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public int IdDanhMuc { get; set; }
    }

}
