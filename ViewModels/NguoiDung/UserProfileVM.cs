namespace WebBanGiayTheThao.ViewModels.NguoiDung
{
    public class UserProfileVM
    {
        public string Username { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public UpdateProfileVM UpdateProfile { get; set; }

    }
}
