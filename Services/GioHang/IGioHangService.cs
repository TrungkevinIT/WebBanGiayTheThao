using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services.GioHang
{
    public interface IGioHangService
    {
        Task ThemSanPhamVaoGioHangAsync(int userId, int sanPhamId, int sizeId, int soLuong);
    }
}
