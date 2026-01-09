using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyThuongHieuController : Controller
    {
        private readonly QuanLyWebBanGiayContext _context;
        public QuanLyThuongHieuController(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TrangQLThuongHieu()
        {
            var danhsach = await _context.ThuongHieus.ToListAsync();
            return View(danhsach);
        }
    }
}
