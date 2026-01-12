using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Filters;
using WebBanGiayTheThao.Services;
using WebBanGiayTheThao.ViewModels.BaoCao;

namespace WebBanGiayTheThao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize] // nếu bạn đang dùng filter phân quyền admin
    public class BaoCaoController : Controller
    {
        private readonly BaoCaoService _baoCaoService;

        public BaoCaoController(BaoCaoService baoCaoService)
        {
            _baoCaoService = baoCaoService;
        }

        /// <summary>
        /// UC049 - Báo cáo thống kê doanh thu
        /// </summary>
        public IActionResult DoanhThu(int? thang, int? nam)
        {
            var now = DateTime.Now;

            int thangChon = thang ?? now.Month;
            int namChon = nam ?? now.Year;

            var vm = _baoCaoService.ThongKeDoanhThuTheoThang(thangChon, namChon);

            return View(vm);
        }
    }
}
