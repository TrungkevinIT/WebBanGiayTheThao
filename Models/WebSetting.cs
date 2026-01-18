using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("WebSetting")]
public partial class WebSetting
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? Logo { get; set; }

    [NotMapped]
    public IFormFile? LogoUpload { get; set; }
    [StringLength(50)]
    [Required(ErrorMessage ="Hotline không được rỗng")]
    public string? Hotline { get; set; }
    [StringLength(50)]
    [Required(ErrorMessage = "Địa Chỉ không được rỗng")]
    public string? DiaChi { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Email không được rỗng")]
    public string? EmailLienHe { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Link fb không được rỗng")]
    public string? Linkfacebook{ get; set; }
    [Required(ErrorMessage ="Mô tả không được rỗng")]
    public string? Mota { get; set; }
}
