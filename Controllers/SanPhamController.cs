using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sp;
        private readonly ILoaiSanPhamService _lsp;
        private readonly IThuongHieuService _thuonghieu;
        public SanPhamController(ISanPhamService sp, ILoaiSanPhamService lsp, IThuongHieuService thuonghieu)
        {
            _sp = sp;
            _lsp = lsp;
            _thuonghieu = thuonghieu;
        }
        public async Task<IActionResult> TrangSanPham(string? search, int? thuongHieuId, int? loaiId, string? sortOrder, int? trangThai, int page = 1)
        {
            int pageSize = 8;

            var item = await _sp.LoadDSSanPham(search, loaiId, thuongHieuId, sortOrder, trangThai, page, pageSize, isAdmin: false);
            int totalPages = (int)Math.Ceiling((double)item.totalCount / pageSize);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;
            ViewBag.TrangThai = trangThai;
            var allThuongHieu = await _thuonghieu.GetAllAsync(null);
            var activeThuongHieu = allThuongHieu.Where(x => x.TrangThai == 1);
            ViewBag.DanhSachThuongHieu = new SelectList(activeThuongHieu, "Id", "TenThuongHieu", thuongHieuId);
            var dsloaisanpham = await _lsp.GetAllLoaiSanPhamAsync(null, 1);
            ViewBag.DanhSachLoaiSanPham = new SelectList(dsloaisanpham, "Id", "TenLoai", loaiId);
            ViewData["ActionName"] = "TrangSanPham";
            return View(item);
        }
        public IActionResult TrangChiTietSanPham(int id)
        {
            return View();
        }
    }
}
