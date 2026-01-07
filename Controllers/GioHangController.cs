using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayTheThao.Controllers
{
    public class GioHangController : Controller
    {
        public IActionResult TrangGioHang()
        {
            return View();
        }
    }
}
