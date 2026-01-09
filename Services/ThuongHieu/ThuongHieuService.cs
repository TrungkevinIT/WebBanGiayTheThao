using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public class ThuongHieuService : IThuongHieuService
    {
        private readonly QuanLyWebBanGiayContext _context;

        public ThuongHieuService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<List<ThuongHieu>> GetAllAsync(string? searchString)
        {
            var query = _context.ThuongHieus.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                query = query.Where(t => t.TenThuongHieu.Contains(searchString) ||
                                         t.XuatXu.Contains(searchString));
            }
            return await query.ToListAsync();
        }

        public async Task<ThuongHieu?> GetByIdAsync(int id)
        {
            return await _context.ThuongHieus.FindAsync(id);
        }

        public async Task CreateAsync(ThuongHieu thuongHieu)
        {
            _context.Add(thuongHieu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ThuongHieu thuongHieu)
        {
            _context.Update(thuongHieu);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTrangThaiAsync(int id, int trangThai)
        {
            var thuongHieu = await _context.ThuongHieus.FindAsync(id);
            if (thuongHieu == null) return false;

            thuongHieu.TrangThai = trangThai;
            _context.Update(thuongHieu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckTenTonTaiAsync(string tenThuongHieu, int? idExclude = null)
        {
            var query = _context.ThuongHieus.AsQueryable();

            if (idExclude.HasValue)
            {
                return await query.AnyAsync(t => t.TenThuongHieu == tenThuongHieu && t.Id != idExclude.Value);
            }
            return await query.AnyAsync(t => t.TenThuongHieu == tenThuongHieu);
        }
    }
}