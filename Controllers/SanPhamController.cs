using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Controllers
{
    public class SanPhamController : Controller
    {
        public IActionResult TrangSanPham()
        {
            return View();
        }
        public IActionResult TrangChiTietSanPham(int id)
        {
            return View();
        }
    }
}
