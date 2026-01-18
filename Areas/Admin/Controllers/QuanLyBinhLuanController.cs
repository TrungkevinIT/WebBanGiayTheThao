using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Controllers.Admin
{
    [Area("Admin")]
    public class QuanLyBinhLuanController : Controller
    {
        private readonly BinhLuanService _service;

        public QuanLyBinhLuanController(BinhLuanService service)
        {
            _service = service;
        }

        public IActionResult TrangQuanLyBinhLuan(int? trangThai, int page = 1)
        {
            var vm = _service.GetDanhSach(trangThai, page);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Duyet(int id)
        {
            _service.Duyet(id);
            return RedirectToAction(nameof(TrangQuanLyBinhLuan));
        }

        [HttpPost]
        public IActionResult An(int id)
        {
            _service.An(id);
            return RedirectToAction(nameof(TrangQuanLyBinhLuan));
        }
    }
}
