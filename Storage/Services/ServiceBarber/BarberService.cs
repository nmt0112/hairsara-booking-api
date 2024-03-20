using Storage.Models;
using Storage.Repositories.RepositoryBarber;
using Storage.Repositories.RepositoryDanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceBarber
{
    public class BarberService : IBarberService
    {
        private readonly IBarberRepository _repository;

        public BarberService(IBarberRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Barber>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Barber> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<List<Barber>> GetByChiNhanhAsync(int chiNhanhId)
        {
            return await _repository.GetByChiNhanhAsync(chiNhanhId);
        }
    }

}
