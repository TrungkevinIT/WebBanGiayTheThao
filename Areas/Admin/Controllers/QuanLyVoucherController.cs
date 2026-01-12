using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]

    public class QuanLyVoucherController : Controller
    {
        private readonly IVoucherServices _services;
        public QuanLyVoucherController(IVoucherServices services)
        {
            _services = services;
        }
        public async Task<IActionResult> TrangQLVoucher(int page = 1)
        {
            int pageSize = 10;
            var (danhsach, totalCount) = await _services.GetAllPagingAsync(page, pageSize);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["ActionName"] = "TrangQLVoucher";
            ViewData["Filters"] = new Dictionary<string, string>(); 

            return View(danhsach);
        }

        [HttpGet]
        public IActionResult TrangThemVoucher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrangThemVoucher(Voucher voucher)
        {
            if (!ModelState.IsValid)
            {
                return View(voucher);
            }

            var errors = await _services.CreateAsync(voucher);

            if (errors != null && errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return View(voucher);
            }

            TempData["ThongBao"] = "Cập nhật thành công!";
            TempData["LoaiThongBao"] = "alert-success";
            return RedirectToAction("TrangQLVoucher");
        }

        [HttpGet]
        public async Task<IActionResult> TrangCapNhatVoucher(int? id)
        {
            if (id == null) return NotFound();
            var voucher = await _services.GetByIdAsync(id.Value);
            if (voucher == null) return NotFound();
            return View(voucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangCapNhatVoucher(int id, [Bind("Id,MaCode,GiaTriDonToiThieu,GiaTriGiam,NgayBatDau,NgayKetThuc,SoLuong,TrangThai")] Voucher voucher)
        {
            if(id != voucher.Id) return NotFound();
            if(await _services.CheckTonTaiAsync(voucher.MaCode,id))
            {
                ModelState.AddModelError("MaCode", "Mã code đã tồn tại.");
            }    
            if(ModelState.IsValid)
            {
                try
                {
                    await _services.UpdateAsync(voucher);
                    TempData["ThongBao"] = "Cập nhật voucher thành công!";
                    TempData["LoaiThongBao"] = "alert-success";
                    return RedirectToAction(nameof(TrangQLVoucher));
                }
                catch(Exception)
                {
                    ModelState.AddModelError("", "Đã có lỗi xảy ra khi cập nhật voucher. Vui lòng thử lại.");
                }
            }   
            return View(voucher);
        }

        [HttpPost]
        public async Task<IActionResult> CapNhatTrangThai(int id, int trangThai)
        {
            bool result = await _services.UpdateTrangThaiAsync(id, trangThai);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Không tìm thấy voucher!" });
        }
    }
}
