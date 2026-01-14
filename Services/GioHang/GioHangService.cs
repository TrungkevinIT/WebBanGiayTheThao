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

        public Task<List<CtgioHang>> GetAllAsync (int UserId)
        {
            return _context.CtgioHangs
                .Where(x => x.UserId == UserId)
                .Include(x => x.SanPham)
                .Include(x => x.Size)
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

    }
}
