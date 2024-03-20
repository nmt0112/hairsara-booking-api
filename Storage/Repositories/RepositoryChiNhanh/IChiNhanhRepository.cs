using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryChiNhanh
{
    public interface IChiNhanhRepository
    {
        Task<List<ChiNhanh>> GetAllAsync();
        Task<ChiNhanh> GetByIdAsync(int id);
        Task<List<ChiNhanh>> GetByIdViTriAsync(int idViTri);
        Task<ChiNhanh> AddAsync(ChiNhanh chiNhanh);
        Task UpdateAsync(ChiNhanh chiNhanh);
        Task DeleteAsync(int id);
    }

}
