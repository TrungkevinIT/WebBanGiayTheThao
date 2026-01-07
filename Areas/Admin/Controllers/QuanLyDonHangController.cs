using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyDonHangController : Controller
    {
        public IActionResult TrangQLDonHang()
        {
            return View();
        }
    }
}
