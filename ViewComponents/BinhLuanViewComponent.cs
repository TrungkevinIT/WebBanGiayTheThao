using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.ViewModels.BinhLuan;

namespace WebBanGiayTheThao.ViewComponents
{
    public class BinhLuanViewComponent : ViewComponent
    {
        private readonly QuanLyWebBanGiayContext _context;

        public BinhLuanViewComponent(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int spId,int trang=1)
        {
            var query = _context.BinhLuans
                .Include(x => x.User)
                .Where(x => x.SanPhamId == spId);
            int tongComment = await query.CountAsync();
            // Tính điểm đánh giá trung bình
            double diemTrungBinh = tongComment > 0 ? (await query.AverageAsync(x => x.Sao)) ?? 0 : 0;
            var pagedComments = await query
                .OrderByDescending(x => x.NgayTao)
                .Skip((trang - 1) * 5)
                .Take(5)
                .ToListAsync();
            var model = new BinhLuanVM
            {
                BinhLuan = pagedComments,
                TongBinhLuan = tongComment,
                TrungBinhDanhGia = Math.Round(diemTrungBinh, 1),
                TrangHienTai = trang,
                TongSoTrang = (int)Math.Ceiling(tongComment / 5.0)
            };
                
            return View("BinhLuanSanPham", model);
        }
    }
}