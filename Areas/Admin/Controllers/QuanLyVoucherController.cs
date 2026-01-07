using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyVoucherController : Controller
    {
        public IActionResult TrangQLVoucher()
        {
            return View();
        }
    }
}
