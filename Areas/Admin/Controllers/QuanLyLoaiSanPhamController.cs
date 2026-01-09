using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]

    public class QuanLyLoaiSanPhamController : Controller
    {
        public IActionResult TrangQLLoaiSanPham()
        {
            return View();
        }
    }
}
