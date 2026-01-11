using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyLoaiSanPhamController : Controller
    {
        public IActionResult TrangQLLoaiSanPham()
        {
            return View();
        }
        public IActionResult TrangThemMoiLoaiSanPham()
        {
            return View();
        }
        public IActionResult TrangCapNhatLoaiSanPham()
        {
            return View();
        }
    }
}
