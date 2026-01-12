using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services.DonHang;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class QuanLyDonHangController : Controller
    {
        private readonly IDonHangService _donHangService;
        public QuanLyDonHangController(IDonHangService donHangService)
        {
            _donHangService = donHangService;
        }
        public IActionResult TrangQLDonHang()
        {
            return View();
        }
    }
}
