using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services.DonHang
{
    public interface IDonHangService
    {
        Task<IEnumerable<HoaDon>> GetAllHoaDonAsync(string searchSDT, int? trangThai, DateTime? ngayDat);
    }
}
