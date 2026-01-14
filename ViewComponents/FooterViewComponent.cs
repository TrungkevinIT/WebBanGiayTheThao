using Microsoft.AspNetCore.Mvc;
using WebBanGiayTheThao.Data;
using System.Linq;

namespace WebBanGiayTheThao.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly QuanLyWebBanGiayContext _context;

        public FooterViewComponent(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var setting = _context.WebSettings.FirstOrDefault();
            return View(setting);
        }
    }
}
