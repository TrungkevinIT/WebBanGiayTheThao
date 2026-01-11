using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<LoaiSanPham>> GetAllLoaiSanPhamAsync(string searchName, int? trangThai)
        {

            var query = _context.LoaiSanPhams.AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(x => x.TenLoai.Contains(searchName));
            }
            if (trangThai.HasValue)
            {
                query = query.Where(x => x.TrangThai == trangThai.Value);
            }
            return await query.OrderBy(x => x.Id).ToListAsync();
        }
        public async Task<LoaiSanPham?> GetLoaiSanPhamByIdAsync(int id)
        {
            return await _context.LoaiSanPhams.FindAsync(id);
        }

    }
}