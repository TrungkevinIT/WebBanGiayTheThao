using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Controllers
{
    public class BinhLuanController : Controller
    {
        private readonly QuanLyWebBanGiayContext _context;

        public BinhLuanController(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult DanhGia([FromBody] BinhLuan model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để đánh giá" });
            }

            var daDanhGia = _context.BinhLuans.Any(x =>
                x.SanPhamId == model.SanPhamId &&
                x.UserId == userId.Value);

            if (daDanhGia)
            {
                return Json(new { success = false, message = "Bạn đã đánh giá sản phẩm này rồi" });
            }

            model.UserId = userId.Value;
            model.NgayTao = DateTime.Now;

            _context.BinhLuans.Add(model);
            _context.SaveChanges();

            return Json(new { success = true, message = "Đánh giá thành công!" });
        }
    }
}
