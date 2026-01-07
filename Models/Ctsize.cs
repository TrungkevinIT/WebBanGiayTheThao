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

    public double? Size { get; set; }

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
