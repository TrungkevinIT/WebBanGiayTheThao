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

            HttpContext.Session.SetInt32("UserId", user!.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("VaiTro", user.VaiTro ?? 2);

            if (user.VaiTro == 1)
                return Redirect("/Admin");

            return RedirectToAction("TrangChu", "Home");
        }

        // ================== ĐĂNG KÝ ==================
        [HttpPost]
        public async Task<IActionResult> DangKy(DangKyVM model)
        {
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
                ModelState.AddModelError(string.Empty, error);
                TempData["ShowRegisterModal"] = true;
                return View("~/Views/Home/TrangChu.cshtml", model);
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

        // ================== QUẢN LÝ TÀI KHOẢN ==================
        [HttpGet]
        public async Task<IActionResult> QuanLyTaiKhoanCaNhan()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("TrangChu", "Home");

            var profile = await _authService.GetProfileAsync(userId.Value);
            if (profile == null)
                return RedirectToAction("TrangChu", "Home");

            return View(profile); // UserProfileVM
        }


        // ================== CẬP NHẬT THÔNG TIN ==================
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("TrangChu", "Home");

            if (!ModelState.IsValid)
            {
                var profile = await _authService.GetProfileAsync(userId.Value);

                ViewBag.ShowUpdateModal = true;
                ViewBag.UpdateProfileModel = model;

                return View("QuanLyTaiKhoanCaNhan", profile);
            }

            var error = await _authService.UpdateProfileAsync(userId.Value, model);
            if (error != null)
            {
                var profile = await _authService.GetProfileAsync(userId.Value);

                // ⭐ CHÍNH DÒNG QUAN TRỌNG
                ModelState.AddModelError(string.Empty, error);

                ViewBag.ShowUpdateModal = true;
                ViewBag.UpdateProfileModel = model;

                return View("QuanLyTaiKhoanCaNhan", profile);
            }

            TempData["ThongBaoSuccess"] = "Cập nhật thông tin thành công";
            return RedirectToAction(nameof(QuanLyTaiKhoanCaNhan));
        }




        // ================== ĐỔI MẬT KHẨU ==================
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("TrangChu", "Home");

            if (!ModelState.IsValid)
            {
                var profile = await _authService.GetProfileAsync(userId.Value);

                ViewBag.ShowChangePasswordModal = true;
                ViewBag.ChangePasswordModel = model;

                return View("QuanLyTaiKhoanCaNhan", profile);
            }

            var error = await _authService.ChangePasswordAsync(
                userId.Value,
                model.OldPassword,
                model.NewPassword
            );

            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);

                var profile = await _authService.GetProfileAsync(userId.Value);

                ViewBag.ShowChangePasswordModal = true;
                ViewBag.ChangePasswordModel = model;

                return View("QuanLyTaiKhoanCaNhan", profile);
            }

            TempData["ThongBaoSuccess"] = "Đổi mật khẩu thành công";
            return RedirectToAction(nameof(QuanLyTaiKhoanCaNhan));
        }



    }
}
