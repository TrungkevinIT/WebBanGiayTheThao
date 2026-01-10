using System.ComponentModel.DataAnnotations;

namespace WebBanGiayTheThao.ViewModels.NguoiDung
{
    public class DangNhapVM
    {
        [Required(ErrorMessage = "Vui lòng nhập username")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; } = null!;
    }
}
