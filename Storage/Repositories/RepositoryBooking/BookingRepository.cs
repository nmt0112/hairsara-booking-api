using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryBooking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Booking.ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Booking.FindAsync(id);
        }
        public async Task<List<Booking>> GetByIdCustomerAsync(int idCustomer)
        {
            return await _context.Booking.Where(cn => cn.IdCustomer == idCustomer).ToListAsync();
        }
        public async Task<List<Booking>> GetByIdBarberAsync(int idBarber)
        {
            return await _context.Booking.Where(cn => cn.IdBarber == idBarber).ToListAsync();
        }
        public async Task<List<Booking>> GetByIdBarberChiNhanhAsync(int idBarber, int idChiNhanh)
        {
            return await _context.Booking.Where(cn => cn.IdBarber == idBarber && cn.IdChiNhanh == idChiNhanh).ToListAsync();
        }
        public async Task AddBookingAsync(Booking booking)
        {
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Booking.FindAsync(bookingId);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }

}
