using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Storage.Models.DTO
{
    public class ChiNhanhDTO
    {
        public string TenChiNhanh { get; set; }
        public string DiaChi { get; set; }
        public int IdViTri { get; set; }
    }
}
