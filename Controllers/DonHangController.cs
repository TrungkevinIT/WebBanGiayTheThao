using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.ViewModels.DonHang;

namespace WebBanGiayTheThao.Controllers
{
    public class DonHangController : Controller
    {
        private readonly QuanLyWebBanGiayContext _context;

        public DonHangController(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        // =========================
        // LỊCH SỬ ĐƠN HÀNG
        // =========================
        public IActionResult LichSu()
        {
            // 🔐 Lấy UserId từ SESSION
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // ❗ Chưa đăng nhập → đá về đăng nhập
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            var orders = _context.HoaDons
                .Where(h => h.UserId == userId.Value)
                .OrderByDescending(h => h.NgayDat)
                .Select(h => new OrderHistoryVM
                {
                    OrderId = h.Id,
                    OrderCode = "HD" + h.Id,
                    OrderDate = h.NgayDat ?? DateTime.Now,
                    TotalAmount = h.TongTien ?? 0,
                    Status = h.TrangThai == 1 ? "Approved"
                           : h.TrangThai == 2 ? "Completed"
                           : "Canceled"
                })
                .ToList();

            return View(orders);
        }

        // =========================
        // CHI TIẾT ĐƠN HÀNG (AJAX)
        // =========================
        [HttpGet]
        public IActionResult ChiTiet(int orderId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Unauthorized();
            }

            var order = _context.HoaDons
                .Include(h => h.CthoaDons)
                .FirstOrDefault(h => h.Id == orderId && h.UserId == userId.Value);

            if (order == null)
                return NotFound();

            return Json(new OrderDetailVM
            {
                OrderCode = "HD" + order.Id,
                OrderDate = order.NgayDat ?? DateTime.Now,
                Receiver = order.HoTenNguoiNhan ?? "",
                Phone = order.Sdtnhan ?? "",
                Address = order.DiaChiNhan ?? "",
                Shipping = 30000,
                Discount = 0,
                Total = order.TongTien ?? 0,
                Status = order.TrangThai == 1 ? "Approved"
                       : order.TrangThai == 2 ? "Completed"
                       : "Canceled"
            });
        }
    }
}
