using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

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

        [HttpGet]
        public IActionResult TrangThemThuongHieu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangThemThuongHieu([Bind("TenThuongHieu,XuatXu,TrangThai")] ThuongHieu thuongHieu)
        {
            bool checkTonTai = await _context.ThuongHieus.AnyAsync(t => t.TenThuongHieu == thuongHieu.TenThuongHieu);
            if (checkTonTai)
            {
                ModelState.AddModelError("TenThuongHieu", "Thương hiệu đã tồn tại!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(thuongHieu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(TrangQLThuongHieu));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Đã có lỗi xảy ra khi lưu dữ liệu.");
                }
            }
            return View(thuongHieu);
        }
    }
}
