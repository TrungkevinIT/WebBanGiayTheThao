using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Controllers
{
    
    public class VoucherController : Controller
    {
        private readonly IVoucherServices _services;
        public VoucherController(IVoucherServices services)
        {   
            _services = services;
        }
        [SessionAuthorize]
        public async Task<IActionResult> TrangVoucher()
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();

            var myVouchers = await _services.GetUserVouchersAvailableAsync(userId);
            ViewBag.MyVouchers = myVouchers;

            ViewBag.DaLuu = myVouchers.GroupBy(x => x.VoucherId.GetValueOrDefault())
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.GiaTriGiamLuu.GetValueOrDefault()).ToList()
                );

            // 3. Danh sách để săn
            var data = await _services.GetActiveAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> LuuVoucher(int voucherId)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            bool ketqua = await _services.LuuVoucherAsync(userId, voucherId);
            return Json(ketqua);
        }
    }
}
