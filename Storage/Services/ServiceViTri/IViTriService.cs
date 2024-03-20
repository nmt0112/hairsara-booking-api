using Storage.Models;
using Storage.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceViTri
{
    public interface IViTriService
    {
        Task<List<ViTri>> GetAllAsync();
        Task<ViTri> GetByIdAsync(int id);
        Task<ViTri> AddAsync(ViTriDTO viTriDTO);
        Task UpdateAsync(ViTri viTri);
        Task DeleteAsync(int id);
    }

}
