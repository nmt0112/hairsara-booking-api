using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryBooking
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task<List<Booking>> GetByIdBarberAsync(int idBarber);
        Task<List<Booking>> GetByIdCustomerAsync(int idCustomer);
        Task<List<Booking>> GetByIdBarberChiNhanhAsync(int idBarber, int idChiNhanh);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
    }

}
