using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.ViewModels
{
    public class UserIndexVM
    {
        public List<User> Users { get; set; } = new();

        public string? Sdt { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
