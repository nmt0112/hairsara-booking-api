using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceBarber
{
    public interface IBarberService
    {
        Task<List<Barber>> GetAllAsync();
        Task<Barber> GetByIdAsync(int id);
        Task<List<Barber>> GetByChiNhanhAsync(int chiNhanhId);
    }

}
