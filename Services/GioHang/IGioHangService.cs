using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IGioHangService
    {
        Task<List<CtgioHang>> GetAllAsync(int UserId);
        Task<bool> XoaItemAsync(int id, int UserId);
        Task<bool> XoaFullAsync(int UserId);
        Task<bool> CapNhatSoLuongAsync(int Id, int ThayDoi, int UserId);
        Task<(List<CtgioHang> List, int TotalCount, decimal TongTien)> GetGioHangPhanTrangAsync(int userId, int page, int pageSize);
    }
}
