using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Data;
using UserEntity = WebBanGiayTheThao.Models.User;


namespace WebBanGiayTheThao.Services
{
    public class AuthService
    {
        private readonly QuanLyWebBanGiayContext _context;

        public AuthService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> LoginAsync(string username, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == username &&
                    u.Password == password &&
                    u.TrangThai == 1   // 1 = hoạt động
                );
        }
    }
}
