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
    }
}
