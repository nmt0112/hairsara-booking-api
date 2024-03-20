using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Repositories.RepositoryChiNhanh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryChiNhanh
{
    public class ChiNhanhRepository : IChiNhanhRepository
    {
        private readonly ApplicationDbContext _context;

        public ChiNhanhRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChiNhanh>> GetAllAsync()
        {
            return await _context.ChiNhanh.ToListAsync();
        }

        public async Task<ChiNhanh> GetByIdAsync(int id)
        {
            return await _context.ChiNhanh.FindAsync(id);
        }
        public async Task<List<ChiNhanh>> GetByIdViTriAsync(int idViTri)
        {
            return await _context.ChiNhanh.Where(cn => cn.IdViTri == idViTri).ToListAsync();
        }
        public async Task<ChiNhanh> AddAsync(ChiNhanh chiNhanh)
        {
            _context.ChiNhanh.Add(chiNhanh);
            await _context.SaveChangesAsync();
            return chiNhanh;
        }

        public async Task UpdateAsync(ChiNhanh chiNhanh)
        {
            _context.Entry(chiNhanh).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chiNhanh = await _context.ChiNhanh.FindAsync(id);

            if (chiNhanh != null)
            {
                _context.ChiNhanh.Remove(chiNhanh);
                await _context.SaveChangesAsync();
            }
        }
    }


}
