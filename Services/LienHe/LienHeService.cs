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
    }
}
