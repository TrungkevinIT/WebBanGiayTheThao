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

        public List<User> GetUsers(string? sdt, int page, int pageSize, out int totalUsers)
        {
            var query = _context.Users.AsQueryable();

            // Tìm theo SĐT
            if (!string.IsNullOrEmpty(sdt))
            {
                query = query.Where(u => u.Sdt.Contains(sdt));
            }

            totalUsers = query.Count();

            return query
                .OrderByDescending(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public bool ChangeUserStatus(int userId, int trangThai, out string message)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                message = "Không tìm thấy người dùng";
                return false;
            }

            user.TrangThai = trangThai;
            _context.SaveChanges();

            message = trangThai == 1
                ? "Mở khóa tài khoản thành công"
                : "Khóa tài khoản thành công";

            return true;
        }
    }
}
