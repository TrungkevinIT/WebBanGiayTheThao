using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services
{
    public interface ISanPhamService
    {
        Task<(List<WebBanGiayTheThao.Models.SanPham> products, int totalCount)> LoadDSSanPham(
         string? search,
         int? loaispid,
         int? thuonghieuid,
         string? gia,
         int? trangthai,
         int page,
         int pagesize
         );
        Task<SanPham?> GetSanPhamById(int id);
        Task CapNhatTrangThaiSanPham(int trangthai);
        Task ThemSanPham(SanPham sp);
        Task CapNhatSanPham(SanPham sp);
        Task<List<SanPham>> GetKieuDangAsync(string maKieuDang);
        
    }
}
