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

        // GET: Admin/QuanLyNguoiDung
        public async Task<IActionResult> TrangQLNguoiDung(string? sdt, int page = 1)
        {
            var result = await _userService.GetUsersAsync(
                sdt,
                page,
                PAGE_SIZE
            );

            var vm = new UserIndexVM
            {
                Users = result.Users,
                Sdt = sdt,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)result.TotalUsers / PAGE_SIZE)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int userId, int trangThai)
        {
            var message = await _userService.ChangeUserStatusAsync(userId, trangThai);
            TempData["Success"] = message;

            return RedirectToAction(nameof(TrangQLNguoiDung));
        }
    }
}
