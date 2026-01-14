using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyWebSettingController : Controller
    {
        private readonly QuanLyWebBanGiayContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // Để lấy đường dẫn lưu ảnh

        public QuanLyWebSettingController(QuanLyWebBanGiayContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> TrangQLWebSetting()
        {
            // Vì chỉ có 1 cấu hình duy nhất, ta luôn lấy ID = 1
            var webSetting = await _context.WebSettings.FirstOrDefaultAsync(m => m.Id == 1);

            // Nếu chưa có (lần đầu chạy), tự tạo mới
            if (webSetting == null)
            {
                webSetting = new WebSetting { Id = 1 };
                _context.Add(webSetting);
                await _context.SaveChangesAsync();
            }

            return View(webSetting);
        }

        [HttpPost]
        public async Task<IActionResult> TrangQLWebSetting(int id, WebSetting webSetting)
        {
            if (id != webSetting.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // --- XỬ LÝ UPLOAD LOGO ---
                    if (webSetting.LogoUpload != null)
                    {
                        // 1. Định nghĩa thư mục lưu: wwwroot/images/logo
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "logo");

                        // Tạo thư mục nếu chưa có
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                        // 2. Tạo tên file độc nhất (tránh trùng lặp)
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + webSetting.LogoUpload.FileName;

                        // 3. Đường dẫn file đầy đủ
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // 4. Copy file vào server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await webSetting.LogoUpload.CopyToAsync(fileStream);
                        }

                        // 5. Gán tên file vào Model để lưu xuống SQL
                        webSetting.Logo = uniqueFileName;
                    }
                    else
                    {
                        // Nếu không upload ảnh mới, giữ nguyên tên ảnh cũ (Cần input hidden ở View)
                        // webSetting.Logo = ... (Đã được binding từ input hidden)
                    }
                    // -------------------------

                    _context.Update(webSetting);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật cấu hình thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.WebSettings.Any(e => e.Id == webSetting.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(webSetting);
        }
    }
}