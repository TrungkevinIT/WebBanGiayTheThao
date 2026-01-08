using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IUserService
    {
        List<User> GetUsers(string? sdt, int page, int pageSize, out int totalUsers);
        bool ChangeUserStatus(int userId, int trangThai, out string message);
    }
}
