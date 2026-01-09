using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.Services;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyThuongHieuController : Controller
    {
        private readonly IThuongHieuService _service;

        public QuanLyThuongHieuController(IThuongHieuService service)
        {
            _service = service;
        }

        public async Task<IActionResult> TrangQLThuongHieu(string? searchString)
        {
            var danhsach = await _service.GetAllAsync(searchString);
            ViewData["CurrentFilter"] = searchString;
            return View(danhsach);
        }

        [HttpGet]
        public IActionResult TrangThemThuongHieu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangThemThuongHieu([Bind("TenThuongHieu,XuatXu,TrangThai")] ThuongHieu thuongHieu)
        {
            if (await _service.CheckTenTonTaiAsync(thuongHieu.TenThuongHieu))
            {
                ModelState.AddModelError("TenThuongHieu", "Thương hiệu đã tồn tại!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateAsync(thuongHieu);
                    TempData["ThongBao"] = "Thêm thương hiệu thành công!";
                    TempData["LoaiThongBao"] = "alert-success"; 
                    return RedirectToAction(nameof(TrangQLThuongHieu));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Đã có lỗi xảy ra khi lưu dữ liệu.");
                }
            }
            return View(thuongHieu);
        }

        [HttpGet]
        public async Task<IActionResult> TrangCapNhatThuongHieu(int? id)
        {
            if (id == null) return NotFound();

            var thuonghieu = await _service.GetByIdAsync(id.Value);
            if (thuonghieu == null) return NotFound();

            return View(thuonghieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrangCapNhatThuongHieu(int id, [Bind("Id,TenThuongHieu,XuatXu,TrangThai")] ThuongHieu thuongHieu)
        {
            if (id != thuongHieu.Id) return NotFound();
            if (await _service.CheckTenTonTaiAsync(thuongHieu.TenThuongHieu, id))
            {
                ModelState.AddModelError("TenThuongHieu", "Tên thương hiệu này đã tồn tại!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(thuongHieu);
                    TempData["ThongBao"] = "Cập nhật thông tin thành công!";
                    TempData["LoaiThongBao"] = "alert-success";
                    return RedirectToAction(nameof(TrangQLThuongHieu));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật.");
                }
            }
            return View(thuongHieu);
        }

        [HttpPost]
        public async Task<IActionResult> CapNhatTrangThai(int id, int trangThai)
        {
            bool result = await _service.UpdateTrangThaiAsync(id, trangThai);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Không tìm thấy thương hiệu!" });
        }
    }
}