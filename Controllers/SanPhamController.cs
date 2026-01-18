using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBanGiayTheThao.Filters; 
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Services.GioHang;

namespace WebBanGiayTheThao.Controllers
{
    public class SanPhamController : Controller
    {

        private readonly ISanPhamService _sanPhamService;
        private readonly IGioHangService _gioHangService;
        private readonly ILoaiSanPhamService _loaiSanPhamService;
        private readonly IThuongHieuService _thuongHieuService;
        public SanPhamController(
            ISanPhamService sanPhamService,
            IGioHangService gioHangService,
            ILoaiSanPhamService loaiSanPhamService,
            IThuongHieuService thuongHieuService)
        {
            _sanPhamService = sanPhamService;
            _gioHangService = gioHangService;
            _loaiSanPhamService = loaiSanPhamService;
            _thuongHieuService = thuongHieuService;
        }


        public async Task<IActionResult> TrangSanPham(string? search, int? thuongHieuId, int? loaiId, string? sortOrder, int? trangThai, int page = 1)
        {
            int pageSize = 8;
            var item = await _sanPhamService.LoadDSSanPham(search, loaiId, thuongHieuId, sortOrder, trangThai, page, pageSize);
            int totalPages = (int)Math.Ceiling((double)item.totalCount / pageSize);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;
            ViewBag.TrangThai = trangThai;
            var allThuongHieu = await _thuongHieuService.GetAllAsync(null);
            var activeThuongHieu = allThuongHieu.Where(x => x.TrangThai == 1);
            ViewBag.DanhSachThuongHieu = new SelectList(activeThuongHieu, "Id", "TenThuongHieu", thuongHieuId);
            var dsloaisanpham = await _loaiSanPhamService.GetAllLoaiSanPhamAsync(null, 1);
            ViewBag.DanhSachLoaiSanPham = new SelectList(dsloaisanpham, "Id", "TenLoai", loaiId);

            ViewData["ActionName"] = "TrangSanPham";
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> TrangChiTietSanPham(int id, int? sizeId)
        {
            if (id < 0) return NotFound();

            var sp = await _sanPhamService.GetSanPhamById(id);
            if (sp == null) return NotFound();

            var cacMauKhac = await _sanPhamService.GetKieuDangAsync(sp.MaKieuDang);
            ViewBag.CacMauKhac = cacMauKhac;

            // Tìm size đó 
            if (sp.Ctsizes != null)
            {
                var sizeDangChon = sp.Ctsizes.FirstOrDefault(x => x.Id == sizeId);

                if (sizeDangChon != null)
                {
                    ViewBag.SoLuongTon = sizeDangChon.SoLuongTon;
                    ViewBag.SizeIdDangChon = sizeId;
                }
            }

            return View(sp);
        }

        // Action: Thêm vào giỏ hàng
        [HttpPost]
        [SessionAuthorize]
        public async Task<IActionResult> ThemVaoGioHang(int productId, int sizeId, int soLuong = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Xử lý trường hợp mất session (dù đã có SessionAuthorize nhưng cẩn thận vẫn hơn)
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _gioHangService.ThemSanPhamVaoGioHangAsync(userId.Value, productId, sizeId, soLuong);
                TempData["Message"] = "Đã thêm vào giỏ hàng thành công!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi: " + ex.Message;
            }

            return RedirectToAction("TrangChiTietSanPham", new { id = productId, sizeId = sizeId });
        }
    }
}