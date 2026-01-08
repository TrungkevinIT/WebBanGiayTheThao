using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IUserService
    {
        Task<(List<User> Users, int TotalUsers)> GetUsersAsync(
            string? sdt,
            int page,
            int pageSize
        );

        Task<string> ChangeUserStatusAsync(int userId, int trangThai);
    }
}
