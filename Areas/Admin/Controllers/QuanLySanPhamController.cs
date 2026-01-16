using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class QuanLySanPhamController : Controller
    {
        private readonly ISanPhamService _sp;
        private readonly IThuongHieuService _thuonghieu;
        private readonly ILoaiSanPhamService _lsp;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public QuanLySanPhamController(ISanPhamService sp, IThuongHieuService thuonghieu, ILoaiSanPhamService lsp, IWebHostEnvironment webHostEnvironment)
        {
            _sp = sp;
            _thuonghieu = thuonghieu;
            _lsp = lsp;
            _webHostEnvironment = webHostEnvironment;
        }

        // DuyKhang edit 15:30 09/01/2026: Chặn cache
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> TrangQLSanPham(string? search, int? thuongHieuId, int? loaiId, string? sortOrder, int? trangThai, int page = 1, bool isAdmin = false)
        {
            int pagesize = 10;

            var sp = await _sp.LoadDSSanPham(search, loaiId, thuongHieuId, sortOrder, trangThai, page, pagesize,isAdmin: true);
            int totalPages = (int)Math.Ceiling((double)sp.totalCount / pagesize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pagesize;
            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;
            ViewBag.TrangThai = trangThai;

            var dsthuonghieu = await _thuonghieu.GetAllAsync(null);
            ViewBag.DanhSachThuongHieu = new SelectList(dsthuonghieu, "Id", "TenThuongHieu", thuongHieuId);
            var dsloaisanpham = await _lsp.GetAllLoaiSanPhamAsync(null,null);
            ViewBag.DanhSachLoaiSanPham = new SelectList(dsloaisanpham, "Id", "TenLoai", loaiId);

            return View(sp.products);
        }

        // Xóa sản phẩm (Xóa mềm)
        public async Task<IActionResult> XoaSanPham(int id, string? search, int? thuongHieuId, int? loaiId, string? sortOrder, int? trangThai, int? page)
        {
            await _sp.CapNhatTrangThaiSanPham(id);
            return RedirectToAction("TrangQLSanPham", new
            {
                search,
                thuongHieuId,
                loaiId,
                sortOrder,
                trangThai,
                page
            });
        }

        // --- TRANG THÊM SẢN PHẨM ---
        [HttpGet]
        public async Task<IActionResult> TrangThemSanPham()
        {
            var dsthuonghieu = await _thuonghieu.GetAllAsync(null);
            var thuongHieuActive = dsthuonghieu.Where(x => x.TrangThai == 1).ToList();
            ViewBag.DanhSachThuongHieu = new SelectList(thuongHieuActive, "Id", "TenThuongHieu");
            var dsloaisanpham = await _lsp.GetAllLoaiSanPhamAsync(null, 1);
            ViewBag.DanhSachLoaiSanPham = new SelectList(dsloaisanpham, "Id", "TenLoai");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrangThemSanPham(SanPham sp)
        {
            // 2. Kiểm tra ảnh bắt buộc (Thủ công)
            if (sp.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Vui lòng chọn ảnh đại diện (Bắt buộc)");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý ảnh đại diện
                    if (sp.ImageFile != null)
                    {
                        string filename = DateTime.Now.Ticks + "_" + Path.GetFileName(sp.ImageFile.FileName);
                        string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", "sanpham", filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await sp.ImageFile.CopyToAsync(stream);
                        }
                        sp.AnhDaiDien = filename;
                    }
                    // Xử lý ảnh phụ
                    if (sp.ListAnhPhu != null && sp.ListAnhPhu.Count > 0)
                    {
                        if (sp.Ctanhs == null) sp.Ctanhs = new List<Ctanh>(); // Khởi tạo list tránh lỗi null

                        foreach (var file in sp.ListAnhPhu)
                        {
                            if (file.Length > 0)
                            {
                                string fname = DateTime.Now.Ticks + "_" + Path.GetFileName(file.FileName);
                                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", "anhphu", fname);
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }
                                sp.Ctanhs.Add(new Ctanh { LinkAnh = fname });
                            }
                        }
                    }

                    // Xử lý Size + Số lượng
                    if (sp.ChiTietSizeNhap != null)
                    {
                        if (sp.Ctsizes == null) sp.Ctsizes = new List<Ctsize>(); // Khởi tạo list tránh lỗi null

                        foreach (var item in sp.ChiTietSizeNhap)
                        {
                            if (item.Size > 0)
                            {
                                sp.Ctsizes.Add(new Ctsize { Size = item.Size, SoLuongTon = item.SoLuongTon });
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    ModelState.AddModelError("TenSanPham",ex.Message);
                }

                sp.TrangThai = 1;
                await _sp.ThemSanPham(sp);
                TempData["ThongBao"] = "Thêm sản phẩm thành công";
                return RedirectToAction("TrangQLSanPham");
            }


            var dsthuonghieu = await _thuonghieu.GetAllAsync(null);
            var thuongHieuActive = dsthuonghieu.Where(x => x.TrangThai == 1).ToList();
            ViewBag.DanhSachThuongHieu = new SelectList(thuongHieuActive, "Id", "TenThuongHieu");
            var dsloaisanpham = await _lsp.GetAllLoaiSanPhamAsync(null, 1);
            ViewBag.DanhSachLoaiSanPham = new SelectList(dsloaisanpham, "Id", "TenLoai");
            return View(sp);
        }

        // --- THÊM NHANH AJAX ---
        [HttpPost]
        public async Task<IActionResult> ThemNhanhDuLieu(string loai, string ten, string xuatxu)
        {
            if (string.IsNullOrWhiteSpace(ten))
            {
                return Json(new { success = false, message = "Tên không được rỗng" });
            }

            ten = ten.Trim();
            if (loai == "Loai")
            {
                var loaiMoi = new LoaiSanPham
                {
                    TenLoai = ten,
                    TrangThai = 1
                };
                 await _lsp.ThemLoaiSanPhamAsync(loaiMoi);
               
                return Json(new { success = true, id = loaiMoi.Id, ten = loaiMoi.TenLoai });
            }
            else if (loai == "ThuongHieu")
            {
                var thMoi = new ThuongHieu
                {
                    TenThuongHieu = ten,
                    XuatXu = xuatxu,
                    TrangThai = 1
                };
                await _thuonghieu.CreateAsync(thMoi);
                return Json(new { success = true, id = thMoi.Id, ten = thMoi.TenThuongHieu });
            }
            return Json(new { success = false, message = "Tên đã tồn tại" });
        }

        // --- TRANG CẬP NHẬT SẢN PHẨM ---
        [HttpGet]
        public async Task<IActionResult> TrangCapNhatSanPham(int id)
        {
            var sp = await _sp.GetSanPhamById(id);
            if (sp == null) return NotFound();

            var dsthuonghieu = await _thuonghieu.GetAllAsync(null);
            ViewBag.DanhSachThuongHieu = new SelectList(dsthuonghieu, "Id", "TenThuongHieu", sp.ThuongHieuId);

            var dsloaisanpham = await _lsp.GetAllLoaiSanPhamAsync(null,1);
            ViewBag.DanhSachLoaiSanPham = new SelectList(dsloaisanpham, "Id", "TenLoai", sp.LoaiSanPhamId);

            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> TrangCapNhatSanPham(SanPham sp)
        {
            // 1. Bỏ qua check bắt buộc chọn ảnh
            ModelState.Remove("ImageFile");
            ModelState.Remove("AnhDaiDien");
            ModelState.Remove("ListAnhPhu");

            if (ModelState.IsValid)
            {
                try { 
                // Lấy sản phẩm gốc từ DB
                var spGoc = await _sp.GetSanPhamById(sp.Id);

                    if (spGoc != null)
                    {
                        spGoc.TenSanPham = sp.TenSanPham;
                        spGoc.MaKieuDang = sp.MaKieuDang;
                        spGoc.DonGia = sp.DonGia;
                        spGoc.MoTa = sp.MoTa;
                        spGoc.ThuongHieuId = sp.ThuongHieuId;
                        spGoc.LoaiSanPhamId = sp.LoaiSanPhamId;
                        spGoc.TrangThai = 1;

                        // A. Cập nhật Ảnh đại diện
                        if (sp.ImageFile != null)
                        {
                            string filename = DateTime.Now.Ticks + "_" + Path.GetFileName(sp.ImageFile.FileName);
                            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", "sanpham", filename);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await sp.ImageFile.CopyToAsync(stream);
                            }
                            spGoc.AnhDaiDien = filename;
                        }

                        // B. Cập nhật Ảnh phụ
                        if (sp.ListAnhPhu != null && sp.ListAnhPhu.Count > 0)
                        {
                            // Xóa ảnh cũ
                            var anhCu = spGoc.Ctanhs.ToList();
                            foreach (var item in anhCu)
                            {
                                if (!string.IsNullOrEmpty(item.LinkAnh))
                                {
                                    string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "anhphu", item.LinkAnh);
                                    if (System.IO.File.Exists(oldPath))
                                    {
                                        System.IO.File.Delete(oldPath);
                                    }
                                }
                                spGoc.Ctanhs.Remove(item);
                            }

                            // Thêm ảnh mới
                            string galleryFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "anhphu");
                            foreach (var file in sp.ListAnhPhu)
                            {
                                if (file.Length > 0)
                                {
                                    string fname = DateTime.Now.Ticks + "_" + Path.GetFileName(file.FileName);
                                    using (var stream = new FileStream(Path.Combine(galleryFolder, fname), FileMode.Create))
                                    {
                                        await file.CopyToAsync(stream);
                                    }
                                    spGoc.Ctanhs.Add(new Ctanh { LinkAnh = fname });
                                }
                            }
                        }

                        // C. Cập nhật Size cũ (Sửa cả số Size và Số lượng)
                        if (sp.Ctsizes != null)
                        {
                            foreach (var item in sp.Ctsizes)
                            {
                                var sizeCu = spGoc.Ctsizes.FirstOrDefault(x => x.Id == item.Id);
                                if (sizeCu != null)
                                {
                                    sizeCu.SoLuongTon = item.SoLuongTon;
                                    sizeCu.Size = item.Size;
                                }
                            }
                        }

                        // D. Thêm Size mới
                        if (sp.ChiTietSizeNhap != null)
                        {
                            foreach (var item in sp.ChiTietSizeNhap)
                            {
                                if (item.Size > 0)
                                {
                                    spGoc.Ctsizes.Add(new Ctsize { Size = item.Size, SoLuongTon = item.SoLuongTon });
                                }
                            }
                        }
                    
                        await _sp.CapNhatSanPham(spGoc);
                        TempData["ThongBao"] = "Cập nhật thành công";
                        return RedirectToAction("TrangQLSanPham");

                    }
                }catch(Exception ex)
                {
                    ModelState.AddModelError("TenSanPham",ex.Message);
                }
            }
   
            var dsthuonghieu = await _thuonghieu.GetAllAsync(null);
            ViewBag.DanhSachThuongHieu = new SelectList(dsthuonghieu, "Id", "TenThuongHieu", sp.ThuongHieuId);
            var dsloaisanpham = await _lsp.GetAllLoaiSanPhamAsync(null,1);
            ViewBag.DanhSachLoaiSanPham = new SelectList(dsloaisanpham, "Id", "TenLoai", sp.LoaiSanPhamId);
           
            var sanPhamCu = await _sp.GetSanPhamById(sp.Id);

            if (sanPhamCu != null)
            {
                sp.AnhDaiDien = sanPhamCu.AnhDaiDien; 
                sp.Ctanhs = sanPhamCu.Ctanhs;

                if (sp.Ctsizes == null) {
                    sp.Ctsizes = sanPhamCu.Ctsizes;
                }
            }

            return View(sp);
        }
    }
}