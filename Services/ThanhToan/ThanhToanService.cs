using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services.ThanhToan
{
    public class ThanhToanService : IThanhToanService
    {
        private readonly QuanLyWebBanGiayContext _context;

        public ThanhToanService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<List<CtgioHang>> GetCheckoutItemsAsync(int userId)
        {
            return await _context.CtgioHangs
                .Include(x => x.SanPham)
                .Include(x => x.Size)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<UserVoucher>> GetVouchersByUserAsync(int userId)
        {
            var now = DateTime.Now;
            return await _context.UserVouchers
                .Where(uv => uv.UserId == userId 
                          && uv.DaSuDung != true
                          && (uv.NgayKetThucLuu == null || uv.NgayKetThucLuu >= now))
                .OrderByDescending(uv => uv.GiaTriGiamLuu)
                .ToListAsync();
        }

        public async Task<(bool Success, string Message, decimal TongGoc, decimal GiamGia, decimal TongThanhToan)> TinhToanAsync(int userId, string? maVoucher)
        {
            decimal tongGoc = 0, giamGia = 0, tongThanhToan = 0;

            var cartItems = await _context.CtgioHangs
                .Include(x => x.SanPham)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any()) return (false, "Giỏ hàng trống", 0, 0, 0);

            tongGoc = cartItems.Sum(x => (decimal)(x.SoLuong ?? 0) * (x.SanPham.DonGia ?? 0));

            if (!string.IsNullOrEmpty(maVoucher))
            {
                string inputCode = maVoucher.Trim().ToUpper();
                var userVoucher = await _context.UserVouchers
                    .FirstOrDefaultAsync(uv => 
                        uv.UserId == userId && 
                        uv.MaCodeLuu == inputCode && 
                        uv.DaSuDung != true);

                if (userVoucher == null) 
                {
                    return (false, "Mã không hợp lệ hoặc bạn không sở hữu!", tongGoc, 0, tongGoc);
                }
                
                if (userVoucher.NgayKetThucLuu.HasValue && userVoucher.NgayKetThucLuu < DateTime.Now) 
                {
                    return (false, "Mã đã hết hạn!", tongGoc, 0, tongGoc);
                }
                
                decimal donToiThieu = userVoucher.DonToiThieu ?? 0;
                if (tongGoc < donToiThieu) 
                {
                    return (false, $"Đơn chưa đủ {donToiThieu:N0}đ!", tongGoc, 0, tongGoc);
                }

                giamGia = userVoucher.GiaTriGiamLuu ?? 0;
                if (giamGia > tongGoc) giamGia = tongGoc;
            }

            tongThanhToan = tongGoc - giamGia;

            return (true, "", tongGoc, giamGia, tongThanhToan);
        }

        public async Task<(bool Success, string Message, int OrderId)> DatHangAsync(HoaDon hoaDon, int userId)
        {
            var ketQua = await TinhToanAsync(userId, hoaDon.MaVoucher);

            if (!ketQua.Success) return (false, ketQua.Message, 0);
            
            if(!string.IsNullOrEmpty(hoaDon.MaVoucher) && !string.IsNullOrEmpty(ketQua.Message))
            {
                 return (false, ketQua.Message, 0);
            }

            hoaDon.TongTien = ketQua.TongThanhToan;
            hoaDon.UserId = userId;
            hoaDon.NgayDat = DateTime.Now;
            hoaDon.TrangThai = 0;

            if (ketQua.GiamGia > 0)
            {
                hoaDon.GhiChu = (hoaDon.GhiChu ?? "") + $" | Voucher: {hoaDon.MaVoucher} (-{ketQua.GiamGia:N0}đ)";
            }

            using (var trans = _context.Database.BeginTransaction())
            {
                try 
                {
                    _context.HoaDons.Add(hoaDon);
                    await _context.SaveChangesAsync();

                    if (ketQua.GiamGia > 0) 
                    {
                        var uv = await _context.UserVouchers.FirstOrDefaultAsync(u => u.UserId == userId && u.MaCodeLuu == hoaDon.MaVoucher);
                        if(uv != null) 
                        { 
                            uv.DaSuDung = true; 
                            uv.NgayNhan = DateTime.Now;
                            _context.UserVouchers.Update(uv);
                        }
                    }

                    var cartItems = await _context.CtgioHangs
                        .Include(x => x.SanPham)
                        .Where(x => x.UserId == userId)
                        .ToListAsync();
                    foreach (var item in cartItems)
                    {
                        _context.CthoaDons.Add(new CthoaDon
                        {
                            HoaDonId = hoaDon.Id,
                            SanPhamId = item.SanPhamId,
                            SizeId = item.SizeId,
                            SoLuong = item.SoLuong ?? 0,
                            DonGia = item.SanPham?.DonGia ?? 0
                        });
                    }
                    _context.CtgioHangs.RemoveRange(cartItems);
                    await _context.SaveChangesAsync();
                    await trans.CommitAsync();
                    return (true, "Thành công", hoaDon.Id);
                }
                catch (Exception ex) 
                { 
                    await trans.RollbackAsync(); 
                    return (false, ex.Message, 0); 
                }
            }
        }
    }
}