using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyWebSettingController : Controller
    {
        private readonly IWebSettingService _webSettingService; 
        private readonly IWebHostEnvironment _webHostEnvironment; // Để lấy đường dẫn lưu ảnh

        public QuanLyWebSettingController(IWebSettingService webSettingService, IWebHostEnvironment webHostEnvironment)
        {
            _webSettingService = webSettingService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> TrangQLWebSetting()
        {
            var web=await _webSettingService.LayThongTinWeb();
            return View(web);
        }

        [HttpPost]
        public async Task<IActionResult> TrangQLWebSetting(WebSetting webSetting)
        {

            if (ModelState.IsValid)
            {
                if (webSetting.LogoUpload!=null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/logo");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + webSetting.LogoUpload.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await webSetting.LogoUpload.CopyToAsync(fileStream);
                    }
                    webSetting.Logo = uniqueFileName;
                }

                    await _webSettingService.CapNhatWebSetting(webSetting);
                    TempData["ThongBao"] = "Cập nhật cấu hình thành công!";
                return RedirectToAction("TrangQLWebSetting");
            }
            return View(webSetting);
        }
    }
}