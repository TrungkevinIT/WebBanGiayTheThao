using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sp;
        public SanPhamController(ISanPhamService sp)
        {
            _sp = sp;
        }
        public async Task<IActionResult> TrangSanPham(string? search, int? loaispid, int? thuonghieuid, string? gia, int page=1)
        {
            int pageSize = 8;
            var item = await _sp.LoadDSSanPham(search, loaispid, thuonghieuid, gia,1, page, pageSize);
            return View(item);
        }
        public IActionResult TrangChiTietSanPham(int id)
        {
            return View();
        }
    }
}
