using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
namespace WebBanGiayTheThao.Services.GioHang
{
    public class GioHangService: IGioHangService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public GioHangService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        public async Task ThemSanPhamVaoGioHangAsync(int userId, int sanPhamId, int sizeId, int soLuong)
        {
            var cartItem = await _context.CtgioHangs.FirstOrDefaultAsync(x => x.UserId == userId&& x.SanPhamId == sanPhamId && x.SizeId == sizeId);

            if (cartItem != null)
            {
                cartItem.SoLuong += soLuong;
                _context.CtgioHangs.Update(cartItem);
            }
            else
            {
                cartItem = new CtgioHang
                {
                    UserId = userId,     
                    SanPhamId = sanPhamId,
                    SizeId = sizeId,
                    SoLuong = soLuong
                };
                _context.CtgioHangs.Add(cartItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
