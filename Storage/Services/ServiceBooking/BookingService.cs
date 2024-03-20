using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Models.DTO;
using Storage.Repositories.RepositoryBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceBooking
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly ApplicationDbContext _context;

        public BookingService(IBookingRepository bookingRepository, ApplicationDbContext context)
        {
            _repository = bookingRepository;
            _context = context;
        }
        public async Task<List<Booking>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<List<Booking>> GetByCustomerAsync(int idCustomer)
        {
            return await _repository.GetByIdCustomerAsync(idCustomer);
        }
        public async Task<List<Booking>> GetByBarberAsync(int idBarber)
        {
            return await _repository.GetByIdBarberAsync(idBarber);
        }
        public async Task<List<Booking>> GetByBarberChiNhanhAsync(int idBarber, int idChiNhanh)
        {
            return await _repository.GetByIdBarberChiNhanhAsync(idBarber, idChiNhanh);
        }

        public async Task<Booking> AddBookingAsync(BookingDTO bookingDTO)
        {
            Booking booking = new Booking
            {
                IdCustomer = bookingDTO.IdCustomer,
                IdChiNhanh = bookingDTO.IdChiNhanh,
                IdDichVu = bookingDTO.IdDichVu,
                IdBarber = bookingDTO.IdBarber,
                ThoiGianBatDau = bookingDTO.ThoiGianBatDau,
            };

            await _repository.AddBookingAsync(booking);

            return booking; // return the added booking
        }
        public async Task ConfirmBookingAsync(int bookingId)
        {
            try
            {
                var booking = await _repository.GetByIdAsync(bookingId);
                if (booking != null)
                {
                    booking.TrangThai = true;
                    await _repository.UpdateBookingAsync(booking);

                    // Tạo một bản ghi trong bảng LichHen và ánh xạ IdBooking
                    LichHen lichHen = new LichHen
                    {
                        IdBooking = booking.Id
                    };
                    await _context.LichHen.AddAsync(lichHen);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error confirming Booking: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }          
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _repository.UpdateBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            await _repository.DeleteBookingAsync(bookingId);
        }
    }

}
