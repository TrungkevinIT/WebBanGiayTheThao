using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.ViewComponents
{
    public class SlideShowViewComponent : ViewComponent
    {
        private readonly QuanLyWebBanGiayContext _context;
        public SlideShowViewComponent(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsslideshow= await _context.SlideShows.OrderByDescending(x=>x.NgayCapNhat).ToListAsync();
            return View("TrangChuSlideShow", dsslideshow);
        }
    }
}
