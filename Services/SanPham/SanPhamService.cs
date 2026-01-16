using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services
{
    public class SanPhamService : ISanPhamService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public SanPhamService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<(List<WebBanGiayTheThao.Models.SanPham> products, int totalCount)> LoadDSSanPham(
         string? search,int? loaispid,int? thuonghieuid,string? gia,int? trangthai,int page,int pagesize, bool isAdmin = false
         )
        {
            var query= _context.SanPhams.Include(sp=>sp.ThuongHieu).Include(sp=>sp.LoaiSanPham).AsQueryable();
            if (!isAdmin)
            {
                query = query.Where(sp => sp.LoaiSanPham.TrangThai == 1 && sp.ThuongHieu.TrangThai == 1);
            }
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
        public async Task ThemSanPham(SanPham sp)
        {
         bool trungten=await _context.SanPhams.AnyAsync(x=>x.TenSanPham.Trim() ==sp.TenSanPham.Trim());
            if (trungten)
            {
                throw new Exception("Tên sản phẩm này đã tồn tại, vui lòng chọn tên khác!");
            }
            _context.SanPhams.Add(sp);
            await _context.SaveChangesAsync();
           
        }
        public async Task CapNhatSanPham(SanPham sp)
        {
            bool trungten = await _context.SanPhams.AnyAsync(x => x.TenSanPham.Trim() == sp.TenSanPham.Trim() && x.Id != sp.Id);
            if (trungten)
            {
                throw new Exception("Tên sản phẩm này đã tồn tại, vui lòng chọn tên khác!");
            }
            _context.SanPhams.Update(sp);
            await _context.SaveChangesAsync();
        }
        public async Task<SanPham?> GetSanPhamById(int id)
        {
            return await _context.SanPhams.Include(x=>x.Ctanhs).Include(x=>x.Ctsizes).FirstOrDefaultAsync(x=>x.Id==id);
        }
        public async Task<List<SanPham>> SanPhamBanChay()
        {
            return await _context.SanPhams
            .Include(sp => sp.LoaiSanPham)
            .Where(sp => sp.TrangThai == 1)
            .OrderByDescending(sp => sp.CthoaDons.Sum(ct => ct.SoLuong))
            .Take(4)

        .ToListAsync();
        }

        public async Task<List<SanPham>> SanPhamMoi()
        {
            return await _context.SanPhams
                .Include(sp => sp.LoaiSanPham)
                .Include(sp => sp.ThuongHieu)
                .Where(sp => sp.TrangThai == 1 && sp.LoaiSanPham.TrangThai == 1 && sp.ThuongHieu.TrangThai == 1)
                .OrderByDescending(sp => sp.Id)
                .Take(2)
                .ToListAsync();
        }
    }

}

