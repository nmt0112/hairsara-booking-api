using Storage.Models;
using Storage.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceDichVu
{
    public interface IDichVuService
    {
        Task<List<DichVu>> GetAllAsync();
        Task<DichVu> GetByIdAsync(int id);
        Task<List<DichVu>> GetByDanhMucAsync(int idDanhMuc);
        Task<DichVu> AddAsync(DichVuDto dichVuDto);
        Task UpdateAsync(DichVu dichVu);
        Task DeleteAsync(int id);
    }
}
