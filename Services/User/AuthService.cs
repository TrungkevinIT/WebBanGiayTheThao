using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Helpers; // dùng PasswordHasher helper
using UserEntity = WebBanGiayTheThao.Models.User;
using WebBanGiayTheThao.ViewModels.NguoiDung;

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
        // ===================== PROFILE USER =====================
        public async Task<UserProfileVM?> GetProfileAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserProfileVM
                {
                    Username = u.Username,
                    FullName = u.HoTen,
                    Email = u.Email,
                    Phone = u.Sdt,
                    Address = u.DiaChi
                })
                .FirstOrDefaultAsync();
        }

        public async Task<string?> UpdateProfileAsync(int userId, UpdateProfileVM model)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return "Người dùng không tồn tại";

            // Email trùng (trừ chính mình)
            if (await _context.Users.AnyAsync(u =>
                u.Email == model.Email && u.Id != userId))
                return "Email đã được sử dụng";

            // SĐT trùng (trừ chính mình)
            if (await _context.Users.AnyAsync(u =>
                u.Sdt == model.Phone && u.Id != userId))
                return "Số điện thoại đã được sử dụng";

            user.HoTen = model.FullName;
            user.Email = model.Email;
            user.Sdt = model.Phone;
            user.DiaChi = model.Address;

            await _context.SaveChangesAsync();
            return null;
        }


        public async Task<string?> ChangePasswordAsync(
            int userId,
            string oldPassword,
            string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return "Người dùng không tồn tại";

            if (!HashHelper.VerifyPassword(user, user.Password, oldPassword))
                return "Mật khẩu hiện tại không đúng";

            user.Password = HashHelper.HashPassword(user, newPassword);
            await _context.SaveChangesAsync();

            return null;
        }

    }
}
