using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.RepositoryDichVu
{
    public class DichVuRepository : IDichVuRepository
    {
        private readonly ApplicationDbContext _context;

        public DichVuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DichVu>> GetAllAsync()
        {
            return await _context.DichVu.ToListAsync();
        }

        public async Task<DichVu> GetByIdAsync(int id)
        {
            return await _context.DichVu.FindAsync(id);
        }
        public async Task<List<DichVu>> GetByIdDichVuAsync(int idDanhMuc)
        {
            return await _context.DichVu.Where(cn => cn.IdDanhMuc == idDanhMuc).ToListAsync();
        }
        public async Task<DichVu> AddAsync(DichVu dichVu)
        {
            _context.DichVu.Add(dichVu);
            await _context.SaveChangesAsync();
            return dichVu;
        }

        public async Task UpdateAsync(DichVu dichVu)
        {
            _context.Entry(dichVu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dichVu = await _context.DichVu.FindAsync(id);
            if (dichVu != null)
            {
                _context.DichVu.Remove(dichVu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
