using Storage.Models;
using Microsoft.EntityFrameworkCore;


namespace Storage.Repositories.RepositoryBarber
{
    public class BarberRepository : IBarberRepository
    {
        private readonly ApplicationDbContext _context;

        public BarberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Barber>> GetAllAsync()
        {
            return await _context.Barber.ToListAsync();
        }
        public async Task<Barber> GetByIdAsync(int id)
        {
            return await _context.Barber.FindAsync(id);
        }
        public async Task<List<Barber>> GetByChiNhanhAsync(int chiNhanhId)
        {
            return await _context.Barber
                .Where(b => b.IdChiNhanhWork == chiNhanhId)
                .ToListAsync();
        }
    }
}