using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Migrations;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyLienHeController : Controller
    {
        private readonly ILienHeService _service;
        public QuanLyLienHeController(ILienHeService service)
        {
            _service = service;
        }   
        public async Task<IActionResult> TrangQLLienHe()
        {
            var danhsach = await _service.GetAllLienHe();
            return View(danhsach);
        }
    }
}
