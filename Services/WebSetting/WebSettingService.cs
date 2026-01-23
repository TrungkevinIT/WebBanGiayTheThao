using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services {
    public class WebSettingService : IWebSettingService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public WebSettingService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        public async Task CapNhatWebSetting(WebSetting webSetting)
        {
            _context.Update(webSetting);
            await _context.SaveChangesAsync();
        }
        public async Task<WebSetting> LayThongTinWeb()
        {
            var setting = await _context.WebSettings.FirstOrDefaultAsync();
            return setting;
        }
    }
}

