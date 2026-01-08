using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services.SanPham;

[Area("Admin")]
public class QuanLySanPhamController : Controller
{
    private readonly ISanPhamSevice _sevice;
    public QuanLySanPhamController(ISanPhamSevice sevice)
    {
        _sevice = sevice;
    }

    public async Task<IActionResult> TrangQLSanPham(
        string? search, int? thuongHieuId, int? loaiId, string? sortOrder, int? trangThai, int page = 1)
    {
        if (!trangThai.HasValue)
        {
            trangThai = 1;
        }
        int pagesize = 2; 

        var sp = await _sevice.LoadDSSanPham(search, loaiId, thuongHieuId, sortOrder, trangThai, page, pagesize);

        int totalPages = (int)Math.Ceiling((double)sp.totalCount / pagesize);

        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pagesize;
 
        ViewBag.Search = search;
        ViewBag.ThuongHieuId = thuongHieuId;
        ViewBag.LoaiId = loaiId;
        ViewBag.SortOrder = sortOrder;
        ViewBag.TrangThai = trangThai;

        return View(sp.products);
    }

    public async Task<IActionResult> XoaSanPham(
        int id, string? search, int? thuongHieuId, int? loaiId, string? sortOrder, int? trangThai, int? page)
    {
        await _sevice.CapNhatTrangThaiSanPham(id);

        return RedirectToAction("TrangQLSanPham", new
        {
            search = search,
            thuongHieuId = thuongHieuId,
            loaiId = loaiId,
            sortOrder = sortOrder,
            trangThai = trangThai,
            page = page
        });
    }
}