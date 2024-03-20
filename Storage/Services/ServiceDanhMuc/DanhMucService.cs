using Storage.Models;
using Storage.Models.DTO;
using Storage.Repositories.RepositoryDanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceDanhMuc
{
    public class DanhMucService : IDanhMucService
    {
        private readonly IDanhMucRepository _repository;

        public DanhMucService(IDanhMucRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DanhMuc>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<DanhMuc> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<DanhMuc> AddAsync(DanhMucDTO danhMucDTO)
        {
            // Chuyển đổi DTO sang đối tượng DichVu
            DanhMuc danhMuc = new DanhMuc
            {
                TenDanhMuc = danhMucDTO.TenDanhMuc,
                MoTaDanhMuc = danhMucDTO.MoTaDanhMuc
            };
            return await _repository.AddAsync(danhMuc);
        }
        public async Task UpdateAsync(DanhMuc danhMuc)
        {
            await _repository.UpdateAsync(danhMuc);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
