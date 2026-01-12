using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using UserEntity = WebBanGiayTheThao.Models.User;

namespace WebBanGiayTheThao.Services.User
{
    public class UserService : IUserService
    {
        private readonly QuanLyWebBanGiayContext _context;

        public UserService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<(List<UserEntity> Users, int TotalUsers)> GetUsersAsync(
            string? sdt,
            int page,
            int pageSize
        )
        {
            var query = _context.Users
                .Where(u => u.VaiTro == 0)
                .AsQueryable();

            if (!string.IsNullOrEmpty(sdt))
            {
                query = query.Where(u => u.Sdt != null && u.Sdt.Contains(sdt));
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
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId && u.VaiTro == 0);

            if (user == null)
            {
                return "Không tìm thấy người dùng hoặc không được phép thao tác admin";
            }

            user.TrangThai = trangThai;
            await _context.SaveChangesAsync();

            return trangThai == 1
                ? "Mở khóa tài khoản thành công"
                : "Khóa tài khoản thành công";
        }
    }
}