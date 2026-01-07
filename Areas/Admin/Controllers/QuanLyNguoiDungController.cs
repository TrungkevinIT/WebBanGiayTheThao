using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyNguoiDungController : Controller
    {
        private readonly QuanLyWebBanGiayContext _context;

        public QuanLyNguoiDungController(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        // =========================
        // GET: /Admin/QuanLyNguoiDung/TrangQLNguoiDung
        // =========================
        public IActionResult TrangQLNguoiDung()
        {
            var users = _context.Users
                                .OrderByDescending(u => u.Id)
                                .ToList();

            return View(users);
        }

        // =========================
        // POST: Khóa / Mở khóa user
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int userId, int trangThai)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                TempData["Error"] = "Không tìm thấy người dùng";
                return RedirectToAction(nameof(TrangQLNguoiDung));
            }

            user.TrangThai = trangThai;

            _context.SaveChanges();

            TempData["Success"] = trangThai == 1
                ? "Mở khóa tài khoản thành công"
                : "Khóa tài khoản thành công";

            return RedirectToAction(nameof(TrangQLNguoiDung));
        }
    }
}
