using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
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
        public async Task<WebBanGiayTheThao.Models.SlideShow?> GetSlideShowById(int id)
        {
            return await _context.SlideShows.FindAsync(id);
        }
        public async Task UpdateSlideShow(WebBanGiayTheThao.Models.SlideShow slideShow) { 
            _context.SlideShows.Update(slideShow);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> KiemTraLinkDaTonTai(string link, int idHienTai)
        {
            return await _context.SlideShows.AnyAsync(x => x.Link == link && x.Id != idHienTai);
        }
    }
}
