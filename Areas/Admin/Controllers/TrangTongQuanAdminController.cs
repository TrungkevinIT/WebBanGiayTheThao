using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Services.DonHang;
using WebBanGiayTheThao.Services.User;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrangTongQuanAdminController : Controller
    {
        private QuanLyWebBanGiayContext _context;
        public TrangTongQuanAdminController(QuanLyWebBanGiayContext context)
        { 
            _context = context;
        }

        public async Task<IActionResult> TongQuanDoiTuong()
        {
            ViewBag.TongQuanSanPham = await _context.SanPhams.CountAsync();
            ViewBag.TongDonHang=await _context.HoaDons.CountAsync();
            ViewBag.TongKhachHang=await _context.Users.CountAsync();
            ViewBag.TongDoanhThu = await _context.HoaDons.Where(x=>x.TrangThai == 1).SumAsync(x=>x.TongTien);
            return View();
        }
    }
}
