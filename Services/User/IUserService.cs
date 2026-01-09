using WebBanGiayTheThao.Models;

using UserEntity = WebBanGiayTheThao.Models.User;
namespace WebBanGiayTheThao.Services.User
{
    public interface IUserService
    {
        Task<(List<UserEntity> Users, int TotalUsers)> GetUsersAsync(
            string? sdt,
            int page,
            int pageSize
        );

        Task<string> ChangeUserStatusAsync(int userId, int trangThai);
    }
}

