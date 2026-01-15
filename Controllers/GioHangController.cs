using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Controllers
{
    [SessionAuthorize]
    public class GioHangController : Controller
    {
        private readonly IGioHangService _service;
        public GioHangController(IGioHangService service)
        {
            _service = service;
        }

        public async Task<IActionResult> TrangGioHangAsync()
        {
            int UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            var danhsach = await _service.GetAllAsync(UserId);
            return View(danhsach);
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
    }
}
