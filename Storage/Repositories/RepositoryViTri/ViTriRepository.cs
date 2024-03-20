using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryViTri
{
    public class ViTriRepository : IViTriRepository
    {
        private readonly ApplicationDbContext _context;

        public ViTriRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ViTri>> GetAllAsync()
        {
            return await _context.ViTri.ToListAsync();
        }
        public async Task<ViTri> GetByIdAsync(int id)
        {
            return await _context.ViTri.FindAsync(id);
        }

        public async Task<ViTri> AddAsync(ViTri viTri)
        {
            _context.ViTri.Add(viTri);
            await _context.SaveChangesAsync();
            return viTri;
        }
        public async Task DeleteAsync(int id)
        {
            var viTri = await _context.ViTri.FindAsync(id);

            if (viTri != null)
            {
                _context.ViTri.Remove(viTri);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(ViTri viTri)
        {
            _context.Entry(viTri).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<ViTri> GetViTriById(int id)
        {
            return await _context.ViTri.FindAsync(id);
        }
    }

}
