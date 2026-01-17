using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Services.ThanhToan;

namespace WebBanGiayTheThao.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly IThanhToanService _service;

        public ThanhToanController(IThanhToanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> TrangThanhToan()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 8;
            var cartItems = await _service.GetCheckoutItemsAsync(userId);
            
            if (!cartItems.Any()) return RedirectToAction("TrangGioHang", "GioHang");

            var ketQua = await _service.TinhToanAsync(userId, null);

            ViewBag.TongGoc = ketQua.TongGoc;
            ViewBag.GiamGia = ketQua.GiamGia;
            ViewBag.TongThanhToan = ketQua.TongThanhToan;
            
            ViewBag.MyVouchers = await _service.GetVouchersByUserAsync(userId);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> DatHang(HoaDon model)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 8;

            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("TongTien");
            ModelState.Remove("NgayDat");
            ModelState.Remove("TrangThai");
            ModelState.Remove("CthoaDons");

            if (!ModelState.IsValid)
            {
                TempData["LoiThanhToan"] = "Vui lòng kiểm tra thông tin!";
                
                var ketQua = await _service.TinhToanAsync(userId, model.MaVoucher);
                
                ViewBag.TongGoc = ketQua.TongGoc;
                ViewBag.GiamGia = ketQua.GiamGia;
                ViewBag.TongThanhToan = ketQua.TongThanhToan;
                
                var cartItems = await _service.GetCheckoutItemsAsync(userId);
                ViewBag.MyVouchers = await _service.GetVouchersByUserAsync(userId);
                return View("TrangThanhToan", cartItems);
            }

            var result = await _service.DatHangAsync(model, userId);

            if (result.Success) 
            {
                return RedirectToAction("DatHangThanhCong", new { id = result.OrderId });
            }
            
            TempData["LoiThanhToan"] = result.Message;
            TempData["VoucherOld"] = model.MaVoucher;

            var kqLai = await _service.TinhToanAsync(userId, model.MaVoucher);
            ViewBag.TongGoc = kqLai.TongGoc;
            ViewBag.GiamGia = kqLai.GiamGia;
            ViewBag.TongThanhToan = kqLai.TongThanhToan;

            var items = await _service.GetCheckoutItemsAsync(userId);
            ViewBag.MyVouchers = await _service.GetVouchersByUserAsync(userId);
            return View("TrangThanhToan", items);
        }

        public IActionResult DatHangThanhCong(int id)
        {
            return View(id);
        }
    }
}