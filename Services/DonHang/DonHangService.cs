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
        public async Task<IEnumerable<HoaDon>> GetAllHoaDonAsync(string searchSDT, int? trangThai, DateTime? ngayDat)
        {

            var query = _context.HoaDons.AsQueryable();
            if (!string.IsNullOrEmpty(searchSDT))
            {
                query = query.Where(x => x.Sdtnhan.Contains(searchSDT));
            }
            if (trangThai.HasValue)
            {
                query = query.Where(x => x.TrangThai == trangThai.Value);
            }
            if (ngayDat.HasValue)
            {

                query = query.Where(x => x.NgayDat.HasValue && x.NgayDat.Value.Date == ngayDat.Value.Date);
            }
            return await query.OrderByDescending(x => x.NgayDat).ToListAsync();
        }
    }
}
