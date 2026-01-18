using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Controllers
{
    public class GioHangController : Controller
    {
        private readonly IGioHangService _service;
        public GioHangController(IGioHangService service)
        {
            _service = service;
        }

        public async Task<IActionResult> TrangGioHang(int page = 1)
        {
            int UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            int pageSize = 10;

            var data = await _service.GetGioHangPhanTrangAsync(UserId, page, pageSize);
            int totalPages = (int)Math.Ceiling((double)data.TotalCount / pageSize);
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["ActionName"] = "TrangGioHang"; 
            ViewBag.TongTienCaGio = data.TongTien;
            return View(data.List);
        }

        public async Task<IActionResult> XoaItem(int id)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            if (userId == 0)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            bool ketqua = await _service.XoaItemAsync(id, userId);
            if (ketqua)
            {
                TempData["GioHangThongBao"] = "Đã xóa sản phẩm thành công!";
            }
            else
            {
                TempData["GioHangLoi"] = "Không thể xóa sản phẩm này!";
            }
            return RedirectToAction("TrangGioHang");
        }

        public async Task<IActionResult> XoaFull(int id)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            if (userId == 0)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            bool ketqua = await _service.XoaFullAsync(userId);
            if (ketqua)
            {
                TempData["GioHangThongBao"] = "Đã xóa tất cả sản phẩm trong giỏ hàng!";
            }
            else
            {
                TempData["GioHangLoi"] = "Giỏ hàng của bạn đang trống!";
            }
            return RedirectToAction("TrangGioHang");
        }

        public async Task<IActionResult> CapNhatSoLuong(int Id, int ThayDoi)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            await _service.CapNhatSoLuongAsync(Id, ThayDoi, userId);
            return RedirectToAction("TrangGioHang");
        }
    }
}
