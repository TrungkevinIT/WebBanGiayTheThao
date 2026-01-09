using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    }
}
