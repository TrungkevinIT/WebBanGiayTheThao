using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;

namespace WebBanGiayTheThao.ViewComponents
{
    public class BinhLuanComponent: ViewComponent
    {
        private readonly QuanLyWebBanGiayContext _context;
        public BinhLuanComponent(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int loaiSpId, int sanPhamId)
        {
            var sp = await _context.SanPhams
                .Where(x => x.LoaiSanPhamId == loaiSpId && x.Id != sanPhamId)
                .OrderBy(r => Guid.NewGuid()) // Random ngẫu nhiên để tạo cảm giác đa dạng
                .Take(4)
                .ToListAsync();

            return View(sp);
        }
    }
}
