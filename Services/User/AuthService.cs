using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using UserEntity = WebBanGiayTheThao.Models.User;
using WebBanGiayTheThao.Helpers; // dùng PasswordHasher helper

namespace WebBanGiayTheThao.Services
{
    public class AuthService
    {
        private readonly QuanLyWebBanGiayContext _context;

        public AuthService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        // ===================== ĐĂNG NHẬP =====================
        public async Task<(UserEntity? user, string? error)> LoginAsync(
            string username,
            string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return (null, "Sai tài khoản hoặc mật khẩu");

            // TÀI KHOẢN BỊ KHÓA
            if (user.TrangThai == 0)
                return (null, "Tài khoản của bạn đã bị khóa");

            // So sánh mật khẩu bằng PasswordHasher
            if (!HashHelper.VerifyPassword(user, user.Password, password))
                return (null, "Sai tài khoản hoặc mật khẩu");

            return (user, null);
        }

        // ===================== ĐĂNG KÝ =====================
        public async Task<string?> RegisterAsync(
            string username,
            string password,
            string hoTen,
            string email,
            string sdt,
            string? diaChi
        )
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                return "Username đã tồn tại";

            if (await _context.Users.AnyAsync(u => u.Email == email))
                return "Email đã tồn tại";

            if (await _context.Users.AnyAsync(u => u.Sdt == sdt))
                return "Số điện thoại đã được sử dụng";

            var user = new UserEntity
            {
                Username = username,
                HoTen = hoTen,
                Email = email,
                Sdt = sdt,
                DiaChi = diaChi,
                VaiTro = 0,
                TrangThai = 1
            };

            // Băm mật khẩu bằng PasswordHasher
            user.Password = HashHelper.HashPassword(user, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return null;
        }
    }
}
