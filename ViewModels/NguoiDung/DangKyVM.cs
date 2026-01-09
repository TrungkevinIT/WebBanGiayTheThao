using System.ComponentModel.DataAnnotations;

namespace WebBanGiayTheThao.ViewModels.NguoiDung
{
    public class DangKyVM
    {
        [Required(ErrorMessage = "Username không được để trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
