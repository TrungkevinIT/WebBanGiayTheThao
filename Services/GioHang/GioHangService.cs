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

    }
}
