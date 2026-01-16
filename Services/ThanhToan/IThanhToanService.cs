using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IThanhToanService
    {
        Task<List<CtgioHang>> GetCheckOutItemAsync(int id);
        Task<List<UserVoucher>> GetVoucherByUserAsync(int UserId);
    }
}
