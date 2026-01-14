using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IGioHangService
    {
        Task<List<CtgioHang>> GetAllAsync(int UserId);
    }
}
