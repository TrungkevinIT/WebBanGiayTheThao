using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Service;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.ViewComponents
{
    public class SlideShowViewComponent : ViewComponent
    {
        private readonly ISlideShowService _slide;
        public SlideShowViewComponent(ISlideShowService slide)
        {
            _slide = slide;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsslideshow= await _slide.LoadDSSlideShow();
            return View("TrangChuSlideShow", dsslideshow);
        }
    }
}
