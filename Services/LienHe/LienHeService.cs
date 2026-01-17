using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public class LienHeService : ILienHeService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public LienHeService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LienHe lienhe)
        {
            lienhe.NgayTao = DateTime.Now;
            _context.LienHes.Add(lienhe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LienHe>> GetAllLienHe()
        {
            return await _context.LienHes.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task CapNhatTrangThaiAsync(int Id, int TrangThaiMoi)
        {
            var lienhe = await _context.LienHes.FindAsync(Id);
            if (lienhe != null)
            {
                lienhe.TrangThai = TrangThaiMoi;
                await _context.SaveChangesAsync();
            }
        }

        public async Task XoaLienHeAsync(int id)
        {
            var lienhe = await _context.LienHes.FindAsync(id);
            if (lienhe != null)
            {
                _context.LienHes.Remove(lienhe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
