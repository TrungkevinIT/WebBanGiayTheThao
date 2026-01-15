using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;

namespace WebBanGiayTheThao.ViewComponents
{
    public class BinhLuanViewComponent : ViewComponent
    {
        private readonly QuanLyWebBanGiayContext _context;

        public BinhLuanViewComponent(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int spId)
        {
            var danhGia = await _context.BinhLuans
                .Include(x => x.User)
                .Where(x => x.SanPhamId== spId) 
                .OrderByDescending(x => x.NgayTao)
                .ToListAsync();
            return View("BinhLuanSanPham", danhGia);
        }
    }
}