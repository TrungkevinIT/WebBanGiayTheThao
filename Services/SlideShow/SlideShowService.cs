using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Service;
namespace WebBanGiayTheThao.Services
{
    public class SlideShowService: ISlideShowService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public SlideShowService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
         public async Task<List<WebBanGiayTheThao.Models.SlideShow>> LoadDSSlideShow()
        {
            var ds = await _context.SlideShows.ToListAsync();
            return (ds);
        }
        public async Task<WebBanGiayTheThao.Models.SlideShow?> GetSlideShowById(int id)
        {
            return await _context.SlideShows.FindAsync(id);
        }
        public async Task UpdateSlideShow(WebBanGiayTheThao.Models.SlideShow slideShow) {
            bool trunglink = _context.SlideShows.Any(x => x.Link == slideShow.Link && x.Id != slideShow.Id);
            if (trunglink)
            {
                throw new Exception("Link này đã tồn tại");
            }
            _context.SlideShows.Update(slideShow);
            await _context.SaveChangesAsync();
        }

    }
}
