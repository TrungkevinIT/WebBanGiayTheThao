using Microsoft.AspNetCore.Mvc;
using System;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Services.GioHang;

namespace WebBanGiayTheThao.Controllers
{
    
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sanPhamService;
        private readonly IGioHangService _gioHangService;
        public SanPhamController(ISanPhamService sanPhamService, IGioHangService gioHang)
        {
            _sanPhamService = sanPhamService;
            _gioHangService = gioHang;
        }
        public async Task<IActionResult> TrangSanPham(string? search, int? loaispid, int? thuonghieuid, string? gia, int page = 1)
        {
            int pageSize = 8; 

            
            var result = await _sanPhamService.LoadDSSanPham(search, loaispid, thuonghieuid, gia, 1, page, pageSize);
            var products = result.products;
            var totalCount = result.totalCount;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.Search = search;
            ViewBag.LoaiSpId = loaispid;
            ViewBag.ThuongHieuId = thuonghieuid;
            ViewBag.Gia = gia;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> TrangChiTietSanPham(int id,int? sizeId)
        {
            if(id<0) return NotFound();
            var sp = await _sanPhamService
                .GetSanPhamById(id);
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
        [HttpPost]
        [SessionAuthorize]
        public async Task<IActionResult> ThemVaoGioHang(int productId, int sizeId, int soLuong = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId").Value;

            try
            {
                await _gioHangService.ThemSanPhamVaoGioHangAsync(userId, productId, sizeId, soLuong);

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
