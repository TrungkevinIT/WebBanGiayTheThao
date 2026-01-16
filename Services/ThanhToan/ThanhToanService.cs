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

        public async Task<List<UserVoucher>> GetVoucherByUserAsync(int UserId)
        {
            var now = DateTime.Now;
            return await _context.UserVouchers
                .Include(uv => uv.Voucher)
                .Where(uv => uv.UserId == UserId &&
                             uv.DaSuDung == false &&
                             uv.NgayKetThucLuu >= now)
                .OrderByDescending(uv=>uv.GiaTriGiamLuu)
                .ToListAsync();

        }
    }
}
