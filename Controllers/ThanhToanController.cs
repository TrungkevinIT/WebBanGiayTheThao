using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Controllers
{
    //[SessionAuthorize]
    public class ThanhToanController : Controller
    {
        public IActionResult TrangThanhToan()
        {
            return View();
        }
    }
}
