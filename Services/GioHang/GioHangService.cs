using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models; 

namespace WebBanGiayTheThao.Services.GioHang
{
    public class GioHangService : IGioHangService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public GioHangService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public Task<List<CtgioHang>> GetAllAsync(int UserId)
        {
            return _context.CtgioHangs
                .Where(x => x.UserId == UserId)
                .Include(x => x.SanPham)
                .Include(x => x.Size)
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<bool> XoaItemAsync(int id, int UserId)
        {
            var item = await _context.CtgioHangs.FindAsync(id);
            if (item == null || item.UserId != UserId)
            {
                return false;
            }
            _context.CtgioHangs.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> XoaFullAsync(int UserId)
        {
            var item = await _context.CtgioHangs 
                .Where(x=>x.UserId == UserId)
                .ToListAsync();
            if(!item.Any())
            {
                return false;
            }
            _context.CtgioHangs.RemoveRange(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CapNhatSoLuongAsync(int Id, int ThayDoi, int UserId)
        {
            var item = await _context.CtgioHangs.FirstOrDefaultAsync(x => x.Id == Id && x.UserId == UserId);
            if (item == null) return false;
            item.SoLuong += ThayDoi;
            if(item.SoLuong < 1) item.SoLuong = 1;
            _context.CtgioHangs.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(List<CtgioHang> List, int TotalCount, decimal TongTien)> GetGioHangPhanTrangAsync(int userId, int page, int pageSize)
        {
            var query = _context.CtgioHangs
                .Where(x => x.UserId == userId)
                .Include(x => x.SanPham)
                .Include(x => x.Size)
                .AsNoTracking();
            int totalCount = await query.CountAsync();
            decimal tongTien = 0;
            if (totalCount > 0)
            {
                tongTien = await query.SumAsync(x => (decimal)(x.SoLuong ?? 0) * (x.SanPham.DonGia ?? 0));
            }
            var list = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (list, totalCount, tongTien);
        }

    }
}
