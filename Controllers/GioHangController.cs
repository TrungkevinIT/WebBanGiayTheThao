using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Controllers
{
    [SessionAuthorize]
    public class GioHangController : Controller
    {
        private readonly IGioHangService _service;
        public GioHangController(IGioHangService service)
        {
            _service = service;
        }

        public async Task<IActionResult> TrangGioHangAsync()
        {
            int UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            var danhsach = await _service.GetAllAsync(UserId);
            return View(danhsach);
        }
    }
}
