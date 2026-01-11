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
            var dsLoaiSanPham = await _loaiSanPhamService.GetAllLoaiSanPhamAsync(Tenloai, trangThai);
            ViewBag.SearchName = Tenloai;
            ViewBag.TrangThai = trangThai;
            return View(dsLoaiSanPham);
        }


        
    }
}