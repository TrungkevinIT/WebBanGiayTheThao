using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;


namespace WebBanGiayTheThao.Services.DonHang
{
    public class DonHangService: IDonHangService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public DonHangService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        
    }
}
