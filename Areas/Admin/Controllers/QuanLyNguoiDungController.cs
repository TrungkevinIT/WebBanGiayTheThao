using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.ViewModels;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyNguoiDungController : Controller
    {
        private readonly IUserService _userService;
        private const int PAGE_SIZE = 20;

        public QuanLyNguoiDungController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult TrangQLNguoiDung(string? sdt, int page = 1)
        {
            var users = _userService.GetUsers(
                sdt,
                page,
                PAGE_SIZE,
                out int totalUsers
            );

            var vm = new UserIndexVM
            {
                Users = users,
                Sdt = sdt,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalUsers / PAGE_SIZE)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int userId, int trangThai)
        {
            _userService.ChangeUserStatus(userId, trangThai, out string message);
            TempData["Success"] = message;
            return RedirectToAction(nameof(TrangQLNguoiDung));
        }
    }
}
