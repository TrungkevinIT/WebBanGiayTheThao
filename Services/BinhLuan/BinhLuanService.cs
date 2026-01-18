using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.ViewModels.BinhLuan;

namespace WebBanGiayTheThao.Services
{
    public class BinhLuanService
    {
        private readonly QuanLyWebBanGiayContext _context;
        private const int PAGE_SIZE = 5;

        public BinhLuanService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public BinhLuanAdminVM GetDanhSach(int? trangThai, int page)
        {
            var query = _context.BinhLuans
                .Include(x => x.User)
                .Include(x => x.SanPham)
                .OrderByDescending(x => x.NgayTao)
                .AsQueryable();

            if (trangThai.HasValue)
            {
                query = query.Where(x => x.TrangThai == trangThai);
            }

            int totalItem = query.Count();

            var data = query
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToList();

            return new BinhLuanAdminVM
            {
                DanhSach = data,
                CurrentPage = page,
                TotalPage = (int)Math.Ceiling((double)totalItem / PAGE_SIZE),
                TrangThai = trangThai
            };
        }

        public void Duyet(int id)
        {
            var bl = _context.BinhLuans.Find(id);
            if (bl == null) return;

            bl.TrangThai = 1;
            _context.SaveChanges();
        }

        public void An(int id)
        {
            var bl = _context.BinhLuans.Find(id);
            if (bl == null) return;

            bl.TrangThai = 2;
            _context.SaveChanges();
        }
    }
}
