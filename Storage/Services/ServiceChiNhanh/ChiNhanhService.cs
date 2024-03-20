using Storage.Models;
using Storage.Models.DTO;
using Storage.Repositories.RepositoryChiNhanh;
using Storage.Repositories.RepositoryViTri;

namespace Storage.Services.ServiceChiNhanh
{
    public class ChiNhanhService : IChiNhanhService
    {
        private readonly IChiNhanhRepository _repository;
        private readonly IViTriRepository _viTriRepository;

        public ChiNhanhService(IChiNhanhRepository repository, IViTriRepository viTriRepository)
        {
            _repository = repository;
            _viTriRepository = viTriRepository;
        }

        public async Task<List<ChiNhanh>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ChiNhanh> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<List<ChiNhanh>> GetByViTriAsync(int idViTri)
        {
            return await _repository.GetByIdViTriAsync(idViTri);
        }
        public async Task AddAsync(ChiNhanhDTO chiNhanhDTO)
        {
            // Tìm DanhMuc tương ứng
            ViTri viTri = await _viTriRepository.GetViTriById(chiNhanhDTO.IdViTri);

            if (viTri == null)
            {
                // Xử lý trường hợp không tìm thấy DanhMuc
                throw new InvalidOperationException("ViTri not found");
            }

            // Chuyển đổi DTO sang đối tượng DichVu
            ChiNhanh chiNhanh = new ChiNhanh
            {
                TenChiNhanh = chiNhanhDTO.TenChiNhanh,
                DiaChi = chiNhanhDTO.DiaChi,
                ViTri = viTri
            };

            // Thêm DichVu vào cơ sở dữ liệu
            await _repository.AddAsync(chiNhanh);
        }

        public async Task UpdateAsync(ChiNhanh chiNhanh)
        {
            await _repository.UpdateAsync(chiNhanh);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }


}
