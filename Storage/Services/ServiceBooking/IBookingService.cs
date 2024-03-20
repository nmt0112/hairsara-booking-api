using Storage.Models;
using Storage.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceBooking
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task<List<Booking>> GetByCustomerAsync(int idCustomer);
        Task<List<Booking>> GetByBarberAsync(int idBarber);
        Task<List<Booking>> GetByBarberChiNhanhAsync(int idBarber, int idChiNhanh);
        Task<Booking> AddBookingAsync(BookingDTO bookingDTO);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
        Task ConfirmBookingAsync(int bookingId);
    }

}
