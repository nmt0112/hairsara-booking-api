using Storage.Models;
using Storage.Models.DTO;
using Storage.Repositories.RepositoryViTri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceViTri
{
    public class ViTriService : IViTriService
    {
        private readonly IViTriRepository _repository;

        public ViTriService(IViTriRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ViTri>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<ViTri> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ViTri> AddAsync(ViTriDTO viTriDTO)
        {
            // Chuyển đổi DTO sang đối tượng DichVu
            ViTri viTri = new ViTri
            {
                TinhThanhPho = viTriDTO.TinhThanhPho
            };
            return await _repository.AddAsync(viTri);
        }
        public async Task UpdateAsync(ViTri viTri)
        {
            await _repository.UpdateAsync(viTri);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }


}
