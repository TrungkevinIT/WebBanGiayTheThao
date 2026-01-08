using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services.SlideShow
{
    public class SlideShowService: ISlideShowService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public SlideShowService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
         public async Task<IEnumerable<WebBanGiayTheThao.Models.SlideShow>> LoadDSSlideShow()
        {
            var ds = await _context.SlideShows.ToListAsync();
            return (ds);
        }
    }
}
