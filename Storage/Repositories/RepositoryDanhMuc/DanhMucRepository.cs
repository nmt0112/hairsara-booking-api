using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryDanhMuc
{
    public class DanhMucRepository : IDanhMucRepository
    {
        private readonly ApplicationDbContext _context;

        public DanhMucRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DanhMuc>> GetAllAsync()
        {
            return await _context.DanhMuc.ToListAsync();
        }
        public async Task<DanhMuc> GetByIdAsync(int id)
        {
            return await _context.DanhMuc.FindAsync(id);
        }

        public async Task<DanhMuc> AddAsync(DanhMuc danhMuc)
        {
            _context.DanhMuc.Add(danhMuc);
            await _context.SaveChangesAsync();
            return danhMuc;
        }
        public async Task DeleteAsync(int id)
        {
            var danhMuc = await _context.DanhMuc.FindAsync(id);

            if (danhMuc != null)
            {
                _context.DanhMuc.Remove(danhMuc);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(DanhMuc danhMuc)
        {
            _context.Entry(danhMuc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<DanhMuc> GetDanhMucById(int id)
        {
            return await _context.DanhMuc.FindAsync(id);
        }
    }
}
