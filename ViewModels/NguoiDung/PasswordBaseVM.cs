using System.ComponentModel.DataAnnotations;

namespace WebBanGiayTheThao.ViewModels.NguoiDung
{
    public class PasswordBaseVM
    {
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải ít nhất 8 ký tự")]
        [RegularExpression(@"^(?=.*[A-Z]).*$",
            ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ hoa")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
