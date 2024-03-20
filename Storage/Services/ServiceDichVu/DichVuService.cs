using Storage.Models;
using Storage.Models.DTO;
using Storage.Repositories.RepositoryDanhMuc;
using Storage.Repositories.RepositoryDichVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceDichVu
{
    public class DichVuService : IDichVuService
    {
        private readonly IDichVuRepository _repository;
        private readonly IDanhMucRepository _danhMucRepository;

        public DichVuService(IDichVuRepository repository, IDanhMucRepository danhMucRepository)
        {
            _repository = repository;
            _danhMucRepository = danhMucRepository;
        }

        public async Task<List<DichVu>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DichVu> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<List<DichVu>> GetByDanhMucAsync(int idDanhMuc)
        {
            return await _repository.GetByIdDichVuAsync(idDanhMuc);
        }
        public async Task<DichVu> AddAsync(DichVuDto dichVuDto)
        {
            // Tìm DanhMuc tương ứng
            DanhMuc danhMuc = await _danhMucRepository.GetDanhMucById(dichVuDto.IdDanhMuc);

            if (danhMuc == null)
            {
                // Xử lý trường hợp không tìm thấy DanhMuc
                throw new InvalidOperationException("DanhMuc not found");
            }

            // Chuyển đổi DTO sang đối tượng DichVu
            DichVu dichVu = new DichVu
            {
                TenDichVu = dichVuDto.TenDichVu,
                MoTa = dichVuDto.MoTa,
                Gia = dichVuDto.Gia,
                DanhMuc = danhMuc
            };

            // Thêm DichVu vào cơ sở dữ liệu
            return await _repository.AddAsync(dichVu);
        }

        public async Task UpdateAsync(DichVu dichVu)
        {
            await _repository.UpdateAsync(dichVu);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
