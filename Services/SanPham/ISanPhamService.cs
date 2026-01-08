using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services.SanPham
{
    public interface ISanPhamService
    {
        Task<(IEnumerable<WebBanGiayTheThao.Models.SanPham> products, int totalCount)> LoadDSSanPham(
         string? search,
         int? loaispid,
         int? thuonghieuid,
         string? gia,
         int? trangthai,
         int page,
         int pagesize
         );

        Task CapNhatTrangThaiSanPham(int trangthai);  
    }
}
