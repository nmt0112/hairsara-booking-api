using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryViTri
{
    public interface IViTriRepository
    {
        Task<List<ViTri>> GetAllAsync();
        Task<ViTri> GetByIdAsync(int id);
        Task<ViTri> AddAsync(ViTri viTri);
        Task UpdateAsync(ViTri viTri);
        Task DeleteAsync(int id);

        Task<ViTri> GetViTriById(int id);
    }
}
