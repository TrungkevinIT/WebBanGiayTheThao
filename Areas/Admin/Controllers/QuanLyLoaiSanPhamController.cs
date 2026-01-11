using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class QuanLyLoaiSanPhamController : Controller
    {
        private readonly ILoaiSanPhamService _loaiSanPhamService;

        public QuanLyLoaiSanPhamController(ILoaiSanPhamService loaiSanPhamService)
        {
            _loaiSanPhamService = loaiSanPhamService;
        }
        [HttpGet]
        public async Task<IActionResult> TrangQLLoaiSanPham(string Tenloai, int? trangThai)
        {
            if (string.IsNullOrWhiteSpace(Tenloai))
            {
                Tenloai = null;
            }
            else
            {
                
                Tenloai = Tenloai.Trim();
            }
            var dsLoaiSanPham = await _loaiSanPhamService.GetAllLoaiSanPhamAsync(Tenloai, trangThai);
            ViewBag.SearchName = Tenloai;
            ViewBag.TrangThai = trangThai;
            return View(dsLoaiSanPham);
        }
        [HttpGet]
        public async Task<IActionResult> TrangCapNhatLoaiSanPham(int? id)
        {
            if (id == null) return NotFound();
            var loaiSanPham = await _loaiSanPhamService.GetLoaiSanPhamByIdAsync(id.Value);
            if (loaiSanPham == null) return NotFound();
            return View(loaiSanPham);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangCapNhatLoaiSanPham(int id, LoaiSanPham loaiSanPham)
        {
            if (id != loaiSanPham.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _loaiSanPhamService.CapNhatLoaiSanPhamAsync(loaiSanPham);

                    TempData["ThongBao"] = "Cập nhật thành công!";
                    TempData["LoaiThongBao"] = "alert-success";
                    return RedirectToAction(nameof(TrangQLLoaiSanPham));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật: " + ex.Message);
                }
            }
            return View(loaiSanPham);
        }
        [HttpGet]
        public async Task<IActionResult> DoiTrangThai(int id)
        {
            var loaiSanPham = await _loaiSanPhamService.GetLoaiSanPhamByIdAsync(id);
            if (loaiSanPham == null)
            {
                TempData["ThongBao"] = "Không tìm thấy loại sản phẩm!";
                TempData["LoaiThongBao"] = "alert-danger";
                return RedirectToAction(nameof(TrangQLLoaiSanPham));
            }
            loaiSanPham.TrangThai = (loaiSanPham.TrangThai == 1) ? 0 : 1;
            await _loaiSanPhamService.CapNhatLoaiSanPhamAsync(loaiSanPham);

            TempData["ThongBao"] = "Đổi trạng thái thành công!";
            TempData["LoaiThongBao"] = "alert-success";
            return RedirectToAction(nameof(TrangQLLoaiSanPham));
        }
        [HttpGet]
        public IActionResult TrangThemMoiLoaiSanPham()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangThemMoiLoaiSanPham(LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _loaiSanPhamService.ThemLoaiSanPhamAsync(loaiSanPham);

                    TempData["ThongBao"] = "Thêm mới thành công!";
                    TempData["LoaiThongBao"] = "alert-success";
                    return RedirectToAction(nameof(TrangQLLoaiSanPham));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi thêm: " + ex.Message);
                }
            }
            return View(loaiSanPham);
        }
    }
}