using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services.SanPham
{
    public interface ISanPhamSevice
    {
        Task<(IEnumerable<WebBanGiayTheThao.Models.SanPham> products, int totalCount)> LoadDSSanPham(
         string? search,
         int? loaispid,
         int? thuonghieuid,
         string? gia,
         int page,
         int pagesize
         );
    }
}
