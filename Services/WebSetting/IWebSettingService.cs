using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services
{
    public interface IWebSettingService
    {
        Task<WebSetting> LayThongTinWeb();
        Task CapNhatWebSetting(WebSetting webSetting);
    }
}
