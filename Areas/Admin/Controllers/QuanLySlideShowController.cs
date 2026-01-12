using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Services.SlideShow;
namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]

    public class QuanLySlideShowController : Controller
    {
        private readonly ISlideShowService _slideShowService;
        private readonly IWebHostEnvironment _env;
        public QuanLySlideShowController(ISlideShowService slideShowService,IWebHostEnvironment env)
        {
            _slideShowService = slideShowService;
            _env = env;
        }

        public async Task<IActionResult> TrangQLSlideShow()
        {
            var p = await _slideShowService.LoadDSSlideShow();
            return View(p);
        }
        [HttpGet]
        public async Task<IActionResult> TrangCapNhatSlideShow(int id) {
            var slide =await _slideShowService.GetSlideShowById(id);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }

        [HttpGet] 
        public async Task<IActionResult> CheckLinkTrung(string link, int id)
        {
            bool isTrung = await _slideShowService.KiemTraLinkDaTonTai(link, id);

            return Json(isTrung);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangCapNhatSlideShow(WebBanGiayTheThao.Models.SlideShow model, IFormFile? fileAnh)
        {
            if (!string.IsNullOrEmpty(model.Link))
            {
               
                bool isTrungLink = await _slideShowService.KiemTraLinkDaTonTai(model.Link, model.Id);

                if (isTrungLink)
                {
                    ModelState.AddModelError("Link", "Link này đã tồn tại, vui lòng nhập link khác.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (fileAnh != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "img","slideshow");

                
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileAnh.FileName);
                string path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileAnh.CopyToAsync(stream);
                }
                model.HinhAnh = fileName;
            }

            await _slideShowService.UpdateSlideShow(model);
            TempData["ThongBao"] = "Cập nhật thành công";
            return RedirectToAction("TrangQLSlideShow");
        }
    }
}
