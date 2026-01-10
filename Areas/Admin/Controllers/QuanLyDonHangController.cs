using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class QuanLyDonHangController : Controller
    {
        public IActionResult TrangQLDonHang()
        {
            return View();
        }
    }
}
