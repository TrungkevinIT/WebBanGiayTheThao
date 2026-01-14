using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class QuanLyBinhLuanController : Controller
    {
        private readonly QuanLyWebBanGiayContext _context;

        public QuanLyBinhLuanController(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        // ================= DANH SÁCH + LỌC =================
        public IActionResult TrangQuanLyBinhLuan(int? trangThai)
        {
            var query = _context.BinhLuans
                .Include(x => x.User)
                .Include(x => x.SanPham)
                .OrderByDescending(x => x.NgayTao)
                .AsQueryable();

            if (trangThai.HasValue)
            {
                query = query.Where(x => x.TrangThai == trangThai);
            }

            ViewBag.TrangThai = trangThai;
            return View(query.ToList());
        }

        // ================= DUYỆT =================
        [HttpPost]
        public IActionResult Duyet(int id)
        {
            var bl = _context.BinhLuans.Find(id);
            if (bl == null) return NotFound();

            bl.TrangThai = 1;
            _context.SaveChanges();

            return RedirectToAction(nameof(TrangQuanLyBinhLuan));
        }

        // ================= ẨN =================
        [HttpPost]
        public IActionResult An(int id)
        {
            var bl = _context.BinhLuans.Find(id);
            if (bl == null) return NotFound();

            bl.TrangThai = 2;
            _context.SaveChanges();

            return RedirectToAction(nameof(TrangQuanLyBinhLuan));
        }
    }
}
