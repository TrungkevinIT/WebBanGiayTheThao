using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public class UserService : IUserService
    {
        private readonly QuanLyWebBanGiayContext _context;

        public UserService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<(List<User> Users, int TotalUsers)> GetUsersAsync(
            string? sdt,
            int page,
            int pageSize
        )
        {
            var query = _context.Users.AsQueryable();

            // Tìm theo SĐT
            if (!string.IsNullOrEmpty(sdt))
            {
                query = query.Where(u => u.Sdt.Contains(sdt));
            }

            var totalUsers = await query.CountAsync();

            var users = await query
                .OrderByDescending(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalUsers);
        }

        public async Task<string> ChangeUserStatusAsync(int userId, int trangThai)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return "Không tìm thấy người dùng";
            }

            user.TrangThai = trangThai;
            await _context.SaveChangesAsync();

            return trangThai == 1
                ? "Mở khóa tài khoản thành công"
                : "Khóa tài khoản thành công";
        }
    }
}
