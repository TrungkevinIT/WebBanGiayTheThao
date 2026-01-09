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
                TempData["LoginError"] = "Vui lòng nhập đầy đủ thông tin";
                return RedirectToAction("TrangChu", "Home");
            }

            var user = await _authService.LoginAsync(model.Username, model.Password);

            if (user == null)
            {
                TempData["LoginError"] = "Sai tài khoản hoặc mật khẩu";
                return RedirectToAction("TrangChu", "Home");
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("VaiTro", user.VaiTro ?? 2);

            if (user.VaiTro == 1)
                return Redirect("/Admin");

            return RedirectToAction("TrangChu", "Home");
        }

        // ================== ĐĂNG XUẤT ==================
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("TrangChu", "Home");
        }

        // ================== ĐĂNG KÝ ==================
        [HttpPost]
        public async Task<IActionResult> DangKy(DangKyVM model)
        {
            // ❌ KHÔNG REDIRECT KHI LỖI
            if (!ModelState.IsValid)
            {
                ViewBag.ShowRegisterModal = true;
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
                ModelState.AddModelError(string.Empty, error);
                ViewBag.ShowRegisterModal = true;
                return View("~/Views/Home/TrangChu.cshtml", model);
            }

            TempData["RegisterSuccess"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("TrangChu", "Home");

        }
    }
}
