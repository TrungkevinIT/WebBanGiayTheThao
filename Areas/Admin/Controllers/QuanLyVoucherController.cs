using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]

    public class QuanLyVoucherController : Controller
    {
        public IActionResult TrangQLVoucher()
        {
            return View();
        }
    }
}
