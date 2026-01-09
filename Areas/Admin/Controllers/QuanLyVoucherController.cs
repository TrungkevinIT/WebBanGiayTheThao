using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyVoucherController : Controller
    {
        private readonly IVoucherServices _services;
        public QuanLyVoucherController(IVoucherServices services)
        {
            _services = services;
        }
        public async Task<IActionResult> TrangQLVoucher()
        {
            var danhsach = await _services.GetAllAsync();
            return View(danhsach);
        }

        [HttpGet]
        public IActionResult TrangThemVoucher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangThemVoucher([Bind("MaCode,GiaTriDonToiThieu,GiaTriGiam,NgayBatDau,NgayKetThuc,SoLuong,TrangThai")] Voucher voucher)
        {
            if (await _services.CheckTonTaiAsync(voucher.MaCode))
            {
                ModelState.AddModelError("MaCode", "Mã code đã tồn tại.");
            }    
            if (ModelState.IsValid)
            {
                try
                {
                    await _services.CreateAsync(voucher);
                    TempData["ThongBao"] = "Thêm voucher thành công!";
                    TempData["LoaiThongBao"] = "alert-success";
                    return RedirectToAction(nameof(TrangQLVoucher));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Đã có lỗi xảy ra khi thêm voucher. Vui lòng thử lại.");
                }
            }
           return View(voucher);
        }


    }
}
