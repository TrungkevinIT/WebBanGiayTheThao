using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services.ThanhToan
{
    public class ThanhToanService : IThanhToanService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public ThanhToanService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<List<CtgioHang>> GetCheckOutItemAsync(int id)
        {
            return await _context.CtgioHangs
                .Include(x => x.SanPham)
                .Include(x => x.Size)
                .Where(c => c.UserId == id)
                .ToListAsync();
        }
    }
}
