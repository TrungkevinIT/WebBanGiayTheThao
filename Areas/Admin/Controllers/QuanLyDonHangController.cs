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
        public async Task<IActionResult> TrangQLDonHang(string searchSDT, int? trangThai, DateTime? ngayDat, int page = 1)
        {
            ViewBag.SearchSDT = searchSDT;
            ViewBag.TrangThai = trangThai;
            ViewBag.NgayDat = ngayDat.HasValue ? ngayDat.Value.ToString("yyyy-MM-dd") : "";
            var danhSach = await _donHangService.GetAllHoaDonAsync(searchSDT, trangThai, ngayDat);
            int pageSize = 15; 
            int totalPages = (int)Math.Ceiling((double)danhSach.Count() / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            var danhSachHienThi = danhSach.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(danhSachHienThi);
        }
        [HttpPost]
        public async Task<IActionResult> CapNhatTrangThai(int id, int trangThai)
        {
            await _donHangService.CapNhatTrangThaiAsync(id, trangThai);
            return RedirectToAction("TrangQLDonHang");
        }
        [HttpGet]
        public async Task<IActionResult> TrangXemChiTietDonHang(int id)
        {

            var hoaDon = await _donHangService.GetHoaDonByIdAsync(id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

    }
}
