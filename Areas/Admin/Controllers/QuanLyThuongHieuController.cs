using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]

    public class QuanLyThuongHieuController : Controller
    {
        public IActionResult TrangQLThuongHieu()
        {
            return View();
        }
    }
}
