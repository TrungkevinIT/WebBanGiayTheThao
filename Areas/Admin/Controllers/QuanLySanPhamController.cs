using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services.SanPham;

[Area("Admin")]
[AdminAuthorize]

public class QuanLySanPhamController : Controller
{
    private readonly ISanPhamService _sevice;
    public QuanLySanPhamController(ISanPhamService sevice)
    {
        _sevice = sevice;
    }
    // DuyKhang edit 15:30 09/01/2026 Hàm này giúp trình duyệt không lưu cache, mỗi lần back sẽ request lại server.
    public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
    {
        Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "0";

        base.OnActionExecuting(context);
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

    public IActionResult ThemSanPham() { 
        return View();
    }
}