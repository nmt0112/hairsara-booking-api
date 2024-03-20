using Storage.Models;
using Storage.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceDanhMuc
{
    public interface IDanhMucService
    {
        Task<List<DanhMuc>> GetAllAsync();
        Task<DanhMuc> GetByIdAsync(int id);
        Task<DanhMuc> AddAsync(DanhMucDTO danhMucDTO);
        Task UpdateAsync(DanhMuc danhMuc);
        Task DeleteAsync(int id);
    }
}
