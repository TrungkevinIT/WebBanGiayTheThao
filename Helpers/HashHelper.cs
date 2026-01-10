//DuyKhang toàn quyền edit file này, mem nào có edit file này hãy comment rõ ràng tên_edittime
using Microsoft.AspNetCore.Identity;
// Sử dụng framework của asp. net core để băm mật khẩu.
namespace WebBanGiayTheThao.Helpers
{
    public static class HashHelper
    {
        public static string HashPassword<TUser>(TUser user, string password)
            where TUser : class
        {
            var hasher = new PasswordHasher<TUser>();
            return hasher.HashPassword(user, password);
        }

        public static bool VerifyPassword<TUser>(TUser user, string hashedPassword, string providedPassword)
            where TUser : class
        {
            var hasher = new PasswordHasher<TUser>();
            var result = hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
