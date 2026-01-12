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
        [HttpGet]
        public async Task<IActionResult> TrangQLDonHang(string searchSDT, int? trangThai, DateTime? ngayDat)
        {
            ViewBag.SearchSDT = searchSDT;
            ViewBag.TrangThai = trangThai;
            ViewBag.NgayDat = ngayDat.HasValue ? ngayDat.Value.ToString("yyyy-MM-dd") : "";
            var danhSach = await _donHangService.GetAllHoaDonAsync(searchSDT, trangThai, ngayDat);
            return View(danhSach);
        }
    }
}
