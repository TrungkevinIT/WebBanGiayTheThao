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

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> DangNhap(DangNhapVM model)
        {
            // Validate rỗng
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                TempData["LoginError"] = "Username và mật khẩu không được để trống";
                return RedirectToAction("TrangChu", "Home");
            }

            var user = await _authService.LoginAsync(model.Username, model.Password);

            if (user == null)
            {
                TempData["LoginError"] = "Sai tài khoản hoặc mật khẩu";
                return RedirectToAction("TrangChu", "Home");
            }

            // Lưu session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("VaiTro", user.VaiTro ?? 2);

            // 🔐 PHÂN QUYỀN
            if (user.VaiTro == 1)
            {
                // Admin
                return Redirect("/Admin");
            }

            // Khách hàng
            return RedirectToAction("TrangChu", "Home");
        }


        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("TrangChu", "Home");
        }
    }
}
