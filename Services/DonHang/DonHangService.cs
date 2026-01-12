using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;


namespace WebBanGiayTheThao.Services.DonHang
{
    public class DonHangService: IDonHangService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public DonHangService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HoaDon>> GetAllHoaDonAsync()
        {
            var query = _context.HoaDons.AsQueryable();
            return await query.OrderByDescending(x => x.NgayDat).ToListAsync();
        }
    }
}
