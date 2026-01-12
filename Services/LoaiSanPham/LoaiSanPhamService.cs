using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public class LoaiSanPhamService : ILoaiSanPhamService
    {
        private readonly QuanLyWebBanGiayContext _context; 
        public LoaiSanPhamService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        public async Task<List<WebBanGiayTheThao.Models.LoaiSanPham>> LoadDSLoaiSanPham()
        {
            return await _context.LoaiSanPhams.Where(x => x.TrangThai == 1).OrderBy(x => x.TenLoai).ToListAsync();
        }
        public async Task<LoaiSanPham?> ThemLoaiNhanh(string ten)
        {
            if(await _context.LoaiSanPhams.AnyAsync(x => x.TenLoai == ten))
            {
                return null; 
            }
            var loai = new LoaiSanPham { TenLoai = ten , TrangThai=1};
            _context.LoaiSanPhams.Add(loai);
            await _context.SaveChangesAsync();
            return loai;
        }
    }
}
