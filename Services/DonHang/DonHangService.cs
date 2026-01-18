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
        public async Task<bool> CapNhatTrangThaiAsync(int id, int trangThai)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null) return false;
            hoaDon.TrangThai = trangThai;
            _context.HoaDons.Update(hoaDon);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<HoaDon?> GetHoaDonByIdAsync(int id)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.User) // Lấy thông tin người đặt
                .Include(h => h.CthoaDons)
                    .ThenInclude(ct => ct.SanPham) // Chỉ cần include Sản phẩm để lấy tên và AnhDaiDien
                .Include(h => h.CthoaDons)
                    .ThenInclude(ct => ct.Size)    // Lấy thông tin Size
                .FirstOrDefaultAsync(m => m.Id == id);

            return hoaDon;
        }
    }
}
