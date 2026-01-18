using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.ViewComponents
{
    public class SanPhamBanChayViewcomponent : ViewComponent
    {
        private readonly ISanPhamService _sanPhamService;
        public SanPhamBanChayViewcomponent(ISanPhamService sanPhamService) { 
            _sanPhamService = sanPhamService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var spbanchay = await _sanPhamService.SanPhamBanChay();
            return View("TrangSanPhamBanChay",spbanchay);
        }
    }
}
