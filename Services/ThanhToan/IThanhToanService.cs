using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IThanhToanService
    {
        Task<List<CtgioHang>> GetCheckoutItemsAsync(int userId);
        Task<List<UserVoucher>> GetVouchersByUserAsync(int userId);
        Task<(bool Success, string Message, decimal TongGoc, decimal GiamGia, decimal TongThanhToan)> TinhToanAsync(int userId, string? maVoucher);
        Task<(bool Success, string Message, int OrderId)> DatHangAsync(HoaDon hoaDon, int userId);
    }
}
