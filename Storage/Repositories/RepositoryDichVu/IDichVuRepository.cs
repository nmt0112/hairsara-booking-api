using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryDichVu
{
    public interface IDichVuRepository
    {
        Task<List<DichVu>> GetAllAsync();
        Task<DichVu> GetByIdAsync(int id);
        Task<List<DichVu>> GetByIdDichVuAsync(int idDanhMuc);
        Task<DichVu> AddAsync(DichVu dichVu);
        Task UpdateAsync(DichVu dichVu);
        Task DeleteAsync(int id);
    }

}
