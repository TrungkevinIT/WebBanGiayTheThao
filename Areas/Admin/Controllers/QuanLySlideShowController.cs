using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Services.SlideShow;
namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLySlideShowController : Controller
    {
        private readonly ISlideShowService _slideShowService;
        public QuanLySlideShowController(ISlideShowService slideShowService )
        {
            _slideShowService = slideShowService;
        }

        public async Task<IActionResult> TrangQLSlideShow()
        {
            var p = await _slideShowService.LoadDSSlideShow();
            return View(p);
        }
    }
}
