using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IThanhToanService
    {
        Task<List<CtgioHang>> GetCheckOutItemAsync(int id);
    }
}
