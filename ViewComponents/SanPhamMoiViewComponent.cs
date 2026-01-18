using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.ViewComponents
{
    public class SanPhamMoiViewComponent : ViewComponent
    {
        private readonly ISanPhamService _spmoi;
        public SanPhamMoiViewComponent(ISanPhamService spmoi)
        {
            _spmoi = spmoi; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await _spmoi.SanPhamMoi();
            return View("TrangSanPhamMoi",item);
        }
    }
}
