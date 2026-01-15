using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.ViewComponents
{
    public class LoaiSanPhamViewComponent : ViewComponent
    {
        private readonly ILoaiSanPhamService _loaiSanPhamService;
        public LoaiSanPhamViewComponent(ILoaiSanPhamService loaiSanPhamService)
        {
            _loaiSanPhamService = loaiSanPhamService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsloaisp = await _loaiSanPhamService.GetDanhMucNoiBatAsync();
            return View("TrangLoaiSanPham",dsloaisp);
        }
    }
}
