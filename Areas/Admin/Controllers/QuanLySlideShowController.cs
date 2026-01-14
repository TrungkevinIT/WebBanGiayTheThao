using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Service;
using WebBanGiayTheThao.Services;
namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]

    public class QuanLySlideShowController : Controller
    {
        private readonly ISlideShowService _slideShowService;
        private readonly IWebHostEnvironment _env;
        public QuanLySlideShowController(ISlideShowService slideShowService, IWebHostEnvironment env)
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
        public async Task<IActionResult> TrangCapNhatSlideShow(int id)
        {
            var slide = await _slideShowService.GetSlideShowById(id);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }

        [HttpPost]
        public async Task<IActionResult> TrangCapNhatSlideShow(WebBanGiayTheThao.Models.SlideShow model, IFormFile? fileAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (fileAnh != null)
                    {
                        string folder = Path.Combine(_env.WebRootPath, "img", "slideshow");
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(fileAnh.FileName);
                        string path = Path.Combine(folder, fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await fileAnh.CopyToAsync(stream);
                        }
                        model.HinhAnh = fileName;
                    }

                    model.NgayCapNhat = DateTime.Now;

                    await _slideShowService.UpdateSlideShow(model);

                    TempData["ThongBao"] = "Cập nhật thành công";
                    return RedirectToAction("TrangQLSlideShow");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Link", ex.Message);
                }
            }

            return View(model);
        }
    }
}