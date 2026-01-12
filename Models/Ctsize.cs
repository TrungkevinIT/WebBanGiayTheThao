using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("CTSize")]
public partial class Ctsize
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage ="Size không được rỗng")]
    [Range(0, double.MaxValue, ErrorMessage = "Size không được nhỏ hơn 0")]
    public double? Size { get; set; }

    [Required(ErrorMessage = "số lượng không được rỗng")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải từ 0 trở lên")]
    public int? SoLuongTon { get; set; }

    public int? SanPhamId { get; set; }

    [InverseProperty("Size")]
    public virtual ICollection<CtgioHang> CtgioHangs { get; set; } = new List<CtgioHang>();

    [InverseProperty("Size")]
    public virtual ICollection<CthoaDon> CthoaDons { get; set; } = new List<CthoaDon>();

    [ForeignKey("SanPhamId")]
    [InverseProperty("Ctsizes")]
    public virtual SanPham? SanPham { get; set; }
}
