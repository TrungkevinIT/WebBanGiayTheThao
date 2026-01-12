using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Controllers
{
    [SessionAuthorize]
    public class GioHangController : Controller
    {
        public IActionResult TrangGioHang()
        {
            return View();
        }
    }
}
