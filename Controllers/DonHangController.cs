using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.ViewModels.DonHang;

namespace WebBanGiayTheThao.Controllers
{
    [SessionAuthorize]
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
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("DangNhap", "NguoiDung");

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
            if (userId == null) return Unauthorized();

            var order = _context.HoaDons
                .Include(h => h.CthoaDons)
                    .ThenInclude(ct => ct.SanPham)
                .Include(h => h.CthoaDons)
                    .ThenInclude(ct => ct.Size)
                .FirstOrDefault(h => h.Id == orderId && h.UserId == userId.Value);

            if (order == null) return NotFound();

            var result = new OrderDetailVM
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
                       : "Canceled",

                Products = order.CthoaDons.Select(ct => new OrderProductVM
                {
                    SanPhamId = ct.SanPhamId,
                    TenSanPham = ct.SanPham.TenSanPham ?? "",
                    Size = ct.SizeId,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.DonGia ?? 0,
                    Anh = ct.SanPham.AnhDaiDien ?? ""
                }).ToList()
            };

            return Json(result);
        }

        // =========================
        // HỦY ĐƠN HÀNG
        // =========================
        [HttpPost]
        public IActionResult HuyDon([FromBody] HuyDonVM model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            var donHang = _context.HoaDons
                .FirstOrDefault(x => x.Id == model.OrderId && x.UserId == userId.Value);

            if (donHang == null)
                return Json(new { success = false, message = "Không tìm thấy đơn hàng" });

            // ❗ CHỈ CHO HỦY KHI TRẠNG THÁI = 1 (ĐÃ DUYỆT)
            if (donHang.TrangThai != 1)
                return Json(new { success = false, message = "Không thể hủy đơn ở trạng thái này" });

            donHang.TrangThai = 3; // Đã hủy
            _context.SaveChanges();

            return Json(new { success = true, message = "Hủy đơn hàng thành công" });
        }
    }
}
