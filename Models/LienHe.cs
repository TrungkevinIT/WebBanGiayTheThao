using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanGiayTheThao.Models
{
    [Table("LienHe")]
    public class LienHe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập họ và tên")]
        [StringLength(100)]
        public string? HoTen { get; set; } 

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email chưa đúng định dạng")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(15)]
        public string? Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung liên hệ")]
        public string? NoiDung { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;



    }
}
