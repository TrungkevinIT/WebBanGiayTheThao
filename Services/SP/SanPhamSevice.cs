using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services.SanPham
{
    public class SanPhamSevice : ISanPhamSevice
    {
        private readonly QuanLyWebBanGiayContext _context;
        public SanPhamSevice(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public (IEnumerable<WebBanGiayTheThao.Models.SanPham> products, int totalCount) LoadDSSanPham(
         string? search,int? loaispid,int? thuonghieuid,string? gia,int page,int pagesize
         )
        {
            var query= _context.SanPhams.Include(sp=>sp.ThuongHieu).Include(sp=>sp.LoaiSanPham).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(sp=>sp.TenSanPham != null && sp.TenSanPham.Contains(search)
                               || sp.MaKieuDang != null && sp.MaKieuDang.Contains(search));
            }
            if (thuonghieuid.HasValue)
            {
                query = query.Where(sp => sp.ThuongHieuId == thuonghieuid);
            }

            if (loaispid.HasValue)
            {
                query = query.Where(sp => sp.LoaiSanPhamId == loaispid);
            }

            switch (gia)
            {
                case "gia_tang": query = query.OrderBy(sp => sp.DonGia); break;
                case "gia_giam": query = query.OrderByDescending(sp => sp.DonGia); break;
                default: query = query.OrderByDescending(sp => sp.Id); break;
            }

            int totalCount = query.Count();

            var items = query.Skip((page - 1) * pagesize)
                             .Take(pagesize)
                             .ToList();
            return (items, totalCount);
        }
    }
}
