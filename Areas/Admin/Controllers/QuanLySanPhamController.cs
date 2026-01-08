using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Services.SanPham;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLySanPhamController : Controller
    {
        private readonly ISanPhamSevice _sevice;
        public QuanLySanPhamController(ISanPhamSevice sevice)
        {
            _sevice = sevice;
        }

        public IActionResult TrangQLSanPham(string? search,int? thuongHieuId,int? loaiId,string? sortOrder,int page = 1)
        {
            int pagesize = 10;
            var sp = _sevice.LoadDSSanPham(search, thuongHieuId, loaiId, sortOrder, page, pagesize);
            int totalPages = (int)Math.Ceiling((double)sp.totalCount / pagesize);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pagesize;
            ViewBag.Search = search;
            ViewBag.ThuongHieuId = thuongHieuId;
            ViewBag.LoaiId = loaiId;
            ViewBag.SortOrder = sortOrder;
            return View(sp.products);
        }
    }
}
