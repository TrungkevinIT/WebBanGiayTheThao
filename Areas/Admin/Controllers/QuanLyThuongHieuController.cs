using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyThuongHieuController : Controller
    {
        public IActionResult TrangQLThuongHieu()
        {
            return View();
        }
    }
}
