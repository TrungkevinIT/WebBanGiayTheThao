using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Controllers
{
    [SessionAuthorize]
    public class ThanhToanController : Controller
    {
        private readonly IThanhToanService _service;
        public ThanhToanController(IThanhToanService service)
        {
            _service = service;
        }
        public IActionResult TrangThanhToan()
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId")!; 
            var cartItems = _service.GetCheckOutItemAsync(UserId).Result;
            if (!cartItems.Any())
            {
                TempData["GioHangloi"] = "Giỏ Hàng của bạn đang trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.";
                return RedirectToAction("TrangGioHang", "GioHang");
            }
            ViewBag.MyVouchers = _service.GetVoucherByUserAsync(UserId).Result;
            return View(cartItems);
        }
    }
}
