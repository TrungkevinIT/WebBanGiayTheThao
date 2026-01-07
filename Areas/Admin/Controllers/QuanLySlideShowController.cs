using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLySlideShowController : Controller
    {
        public IActionResult TrangQLSlideShow()
        {
            return View();
        }
    }
}
