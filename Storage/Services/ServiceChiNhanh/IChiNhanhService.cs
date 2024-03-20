using Storage.Models;
using Storage.Models.DTO;

namespace Storage.Services.ServiceChiNhanh
{
    public interface IChiNhanhService
    {
        Task<List<ChiNhanh>> GetAllAsync();
        Task<ChiNhanh> GetByIdAsync(int id);
        Task<List<ChiNhanh>> GetByViTriAsync(int idViTri);
        Task AddAsync(ChiNhanhDTO chiNhanhDTO);
        Task UpdateAsync(ChiNhanh chiNhanh);
        Task DeleteAsync(int id);
    }

}
