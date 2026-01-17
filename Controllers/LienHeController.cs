using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Controllers
{
    public class LienHeController : Controller
    {
        private readonly ILienHeService _service;
        public LienHeController(ILienHeService service)
        {
            _service = service;
        }
        public IActionResult TrangLienHe()
        {
            return View();
        }

        public async Task<IActionResult> GuiLienHe(LienHe model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(model);
                    TempData["SuccessMessage"] = "Gửi liên hệ thành công!";
                    return RedirectToAction("TrangLienHe");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Đã có lỗi xảy ra khi gửi liên hệ: " + ex.Message);
                }
            }
            return View("TrangLienHe",model);
        }



    }
}
