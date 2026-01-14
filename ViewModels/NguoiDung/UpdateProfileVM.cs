using System.ComponentModel.DataAnnotations;
namespace WebBanGiayTheThao.ViewModels.NguoiDung;
public class UpdateProfileVM
{
    [Required(ErrorMessage = "Họ tên không được để trống")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$",
        ErrorMessage = "Email phải có định dạng @gmail.com")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ chứa số")]
    public string Phone { get; set; }

    public string? Address { get; set; }
}
