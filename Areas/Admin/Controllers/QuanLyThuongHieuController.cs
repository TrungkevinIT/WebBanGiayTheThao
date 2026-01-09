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

        public async Task<IActionResult> TrangQLThuongHieu(string? searchString)
        {
            var query = _context.ThuongHieus.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                query = query.Where(t => t.TenThuongHieu.Contains(searchString) ||
                                 t.XuatXu.Contains(searchString));
            }
            var danhsach = await query.ToListAsync();
            ViewData["CurrentFilter"] = searchString;
            return View(danhsach);
        }
    }
}
