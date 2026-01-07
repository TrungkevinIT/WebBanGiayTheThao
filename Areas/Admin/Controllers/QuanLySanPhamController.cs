using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLySanPhamController : Controller
    {
        public IActionResult TrangQLSanPham()
        {
            return View();
        }
    }
}
