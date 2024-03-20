using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Storage.Models.DTO
{
    public class BookingDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        [JsonIgnore]
        public bool TrangThai { get; set; }
        [JsonIgnore]
        public int IdCustomer { get; set; }
        [JsonIgnore]
        public int IdChiNhanh { get; set; }
        [JsonIgnore]
        public int IdDichVu { get; set; }
        [JsonIgnore]
        public int IdBarber { get; set; }
    }
}
