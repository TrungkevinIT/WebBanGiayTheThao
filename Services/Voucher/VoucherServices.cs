using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public class VoucherServices : IVoucherServices
    {
        private readonly QuanLyWebBanGiayContext _context;
        public VoucherServices(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public Task<List<Voucher>> GetAllAsync()
        {
            return _context.Vouchers
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task CreateAsync(Voucher voucher)
        {
            _context.Add(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckTonTaiAsync(string maCode, int? idExclude = null)
        {
            var query = _context.Vouchers.AsQueryable();
            if (idExclude.HasValue)
            {
                query = query.Where(v => v.Id != idExclude.Value);
            }
            return await query.AnyAsync(v => v.MaCode == maCode);
        }
    }
}
