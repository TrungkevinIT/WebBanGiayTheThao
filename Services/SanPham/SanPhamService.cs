using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services.SanPham
{
    public class SanPhamService : ISanPhamService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public SanPhamService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<WebBanGiayTheThao.Models.SanPham> products, int totalCount)> LoadDSSanPham(
         string? search,int? loaispid,int? thuonghieuid,string? gia,int? trangthai,int page,int pagesize
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

            if (trangthai.HasValue)
            {
                query=query.Where(sp=>sp.TrangThai == trangthai);
            }

            switch (gia)
            {
                case "gia_tang": query = query.OrderBy(sp => sp.DonGia); break;
                case "gia_giam": query = query.OrderByDescending(sp => sp.DonGia); break;
                default: query = query.OrderByDescending(sp => sp.Id); break;
            }

            int totalCount = await query.CountAsync();

            var items =await query.Skip((page - 1) * pagesize)
                             .Take(pagesize)
                             .ToListAsync();
            return (items, totalCount);
        }
        public async Task CapNhatTrangThaiSanPham(int id)
        {
            var sp =await _context.SanPhams.FindAsync(id);
            if (sp != null)
            {
                //hoat dong
                if (sp.TrangThai == 1)
                {
                    sp.TrangThai = 0;
                    _context.SanPhams.Update(sp);
                }
                else
                {
                    sp.TrangThai = 1;
                    _context.SanPhams.Update(sp);
                }

               await _context.SaveChangesAsync();
            }
        }
    }
}
