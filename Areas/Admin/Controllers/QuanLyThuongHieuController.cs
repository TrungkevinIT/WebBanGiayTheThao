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

        [HttpGet]
        public async Task<IActionResult> TrangCapNhatThuongHieu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var thuonghieu = await _context.ThuongHieus.FindAsync(id);
            if (thuonghieu == null)
            {
                return NotFound();
            }
            return View(thuonghieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangCapNhatThuongHieu(int id, [Bind("Id,TenThuongHieu,XuatXu,TrangThai")] ThuongHieu thuongHieu)
        {
            if (id != thuongHieu.Id)
            {
                return NotFound();
            }

            bool checkTenTrung = await _context.ThuongHieus
                                                .AnyAsync(t => t.TenThuongHieu == thuongHieu.TenThuongHieu && t.Id != thuongHieu.Id);
            if (checkTenTrung)
            {
                ModelState.AddModelError("TenThuongHieu", "Tên thương hiệu này đã tồn tại!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thuongHieu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(TrangQLThuongHieu));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuongHieuExists(thuongHieu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(thuongHieu);
        }

        private bool ThuongHieuExists(int id)
        {
            return _context.ThuongHieus.Any(e => e.Id == id);
        }
    }
}
