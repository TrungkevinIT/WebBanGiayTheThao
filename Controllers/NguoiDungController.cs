using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.ViewModels.NguoiDung;

namespace WebBanGiayTheThao.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly AuthService _authService;

        public NguoiDungController(AuthService authService)
        {
            _authService = authService;
        }

        // ================== ĐĂNG NHẬP ==================
        [HttpPost]
        public async Task<IActionResult> DangNhap(DangNhapVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ThongBao"] = "Vui lòng nhập đầy đủ thông tin";
                return RedirectToAction("TrangChu", "Home");
            }

            var (user, error) = await _authService.LoginAsync(
                model.Username,
                model.Password
            );

            if (error != null)
            {
                TempData["ThongBao"] = error;
                return RedirectToAction("TrangChu", "Home");
            }

            //  ĐĂNG NHẬP THÀNH CÔNG
            HttpContext.Session.SetInt32("UserId", user!.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("VaiTro", user.VaiTro ?? 2);

            if (user.VaiTro == 1)
                return Redirect("/Admin");

            return RedirectToAction("TrangChu", "Home");
        }
        // Controller DangKy
        [HttpPost]
        public async Task<IActionResult> DangKy(DangKyVM model)
        {
            // Khi xuất hiện lỗi validate sẽ hiện lại form đăng ký với thông báo lỗi
            if (!ModelState.IsValid)
            {
                TempData["ShowRegisterModal"] = true;
                return View("~/Views/Home/TrangChu.cshtml", model);
            }

            var error = await _authService.RegisterAsync(
                model.Username,
                model.Password,
                model.HoTen,
                model.Email,
                model.Sdt,
                model.DiaChi
            );

            if (error != null)
            {
                TempData["RegisterError"] = error;
                TempData["ShowRegisterModal"] = true;
                return RedirectToAction("TrangChu", "Home");
            }

            TempData["ThongBao"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";
            return RedirectToAction("TrangChu", "Home");
        }
        // ================== ĐĂNG XUẤT ==================
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("TrangChu", "Home");
        }
    }
}