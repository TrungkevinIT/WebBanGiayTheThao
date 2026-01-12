using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services
{
    public interface ILoaiSanPhamService
    {
        Task<List<WebBanGiayTheThao.Models.LoaiSanPham>> LoadDSLoaiSanPham();
        Task<LoaiSanPham?> ThemLoaiNhanh(string ten);
    }
}
