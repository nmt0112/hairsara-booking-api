using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryBarber
{
    public interface IBarberRepository
    {
        Task<List<Barber>> GetAllAsync();
        Task<Barber> GetByIdAsync(int id);
        Task<List<Barber>> GetByChiNhanhAsync(int chiNhanhId);
    }

}
