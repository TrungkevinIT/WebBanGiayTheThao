using System.ComponentModel.DataAnnotations;
namespace WebBanGiayTheThao.ViewModels.NguoiDung;
public class ChangePasswordVM
{
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
    [MinLength(8, ErrorMessage = "Mật khẩu phải ít nhất 8 ký tự")]
    [RegularExpression(@"^(?=.*[A-Z]).*$",
        ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ hoa")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu mới")]
    [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp")]
    public string ConfirmPassword { get; set; }
}
