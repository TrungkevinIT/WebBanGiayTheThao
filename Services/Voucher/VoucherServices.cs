using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public Task<List<Voucher>> GetActiveAsync()
        {
            var now = DateTime.Now;
            return _context.Vouchers
                .AsNoTracking()
                .Where(v => v.TrangThai == 1 
                && (v.NgayBatDau == null || v.NgayBatDau <= now)
                && (v.NgayKetThuc == null || v.NgayKetThuc >= now))
                .OrderByDescending(v => v.GiaTriGiam)
                .ToListAsync();
        }

        public async Task<Dictionary<int, List<decimal>>> GetDaLuuInfoAsync(int userId)
        {
            var list = await _context.UserVouchers
                .Where(uv => uv.UserId == userId)
                .Select(uv => new { uv.VoucherId, uv.GiaTriGiamLuu })
                .ToListAsync();

            return list.GroupBy(x => x.VoucherId.GetValueOrDefault())
                       .ToDictionary(g => g.Key, g => g.Select(x => x.GiaTriGiamLuu.GetValueOrDefault()).ToList());
        }
        public async Task<List<UserVoucher>> GetUserVouchersAvailableAsync(int userId)
        {
            return await _context.UserVouchers
                .Include(uv => uv.Voucher)
                .Where(uv => uv.UserId == userId && uv.DaSuDung == false)
                .OrderByDescending(uv => uv.NgayNhan)
                .ToListAsync();
        }

        public async Task<bool> LuuVoucherAsync(int userId, int voucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(voucherId);
            if (voucher == null || voucher.SoLuong < 1) return false;

            bool DaLuuBanMoiNhat = await _context.UserVouchers.AnyAsync(uv => uv.UserId == userId
                 && uv.VoucherId == voucherId
                 && uv.MaCodeLuu == voucher.MaCode
                 && uv.GiaTriGiamLuu == voucher.GiaTriGiam
                 && uv.NgayBatDauLuu == voucher.NgayBatDau
                 && uv.NgayKetThucLuu == voucher.NgayKetThuc);

            if (DaLuuBanMoiNhat) return false;

            var userVoucher = new UserVoucher
            {
                UserId = userId,
                VoucherId = voucherId,
                NgayNhan = DateTime.Now,
                DaSuDung = false,
                MaCodeLuu = voucher.MaCode,
                DonToiThieu = voucher.GiaTriDonToiThieu,
                GiaTriGiamLuu = voucher.GiaTriGiam,
                NgayBatDauLuu = voucher.NgayBatDau,
                NgayKetThucLuu = voucher.NgayKetThuc

            };
            _context.UserVouchers.Add(userVoucher);

            voucher.SoLuong -= 1;
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<string, string>?> CreateAsync(Voucher voucher)
        {
            var errors = new Dictionary<string, string>();

            if (voucher.GiaTriGiam < 0)
            {
                errors.Add("GiaTriGiam", "Giá trị giảm không được là số âm!");
            }

            if (voucher.GiaTriGiam.HasValue && voucher.GiaTriDonToiThieu.HasValue && voucher.GiaTriDonToiThieu > 0)
            {
                if (voucher.GiaTriGiam >= voucher.GiaTriDonToiThieu)
                {
                    if (!errors.ContainsKey("GiaTriGiam"))
                    {
                        errors.Add("GiaTriGiam", "Giá trị giảm phải nhỏ hơn đơn hàng tối thiểu!");
                    }
                }
            }

            if (voucher.SoLuong < 1)
            {
                errors.Add("SoLuong", "Số lượng phát hành phải từ 1 trở lên!");
            }

            if (voucher.NgayBatDau.HasValue && voucher.NgayKetThuc.HasValue)
            {
                if (voucher.NgayKetThuc <= voucher.NgayBatDau)
                {
                    errors.Add("NgayKetThuc", "Ngày kết thúc phải lớn hơn ngày bắt đầu!");
                }
            }

            if (errors.Count > 0)
            {
                return errors;
            }
            _context.Add(voucher);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<Voucher?> GetByIdAsync(int id)
        {
            return await _context.Vouchers.FindAsync(id);
        }

        public async Task UpdateAsync(Voucher voucher)
        {
            _context.Update(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTrangThaiAsync(int id, int trangThai)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null) return false;

            voucher.TrangThai = trangThai;
            _context.Update(voucher);
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<(List<Voucher> List, int TotalCount)> GetAllPagingAsync(int page, int pageSize)
        {
            var query = _context.Vouchers.AsNoTracking().OrderBy(x => x.Id); 
            int totalCount = await query.CountAsync();
            var list = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (list, totalCount);
        }
    }
}
