using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[PrimaryKey("HoaDonId", "SanPhamId", "SizeId")]
[Table("CTHoaDon")]
public partial class CthoaDon
{
    [Key]
    public int HoaDonId { get; set; }

    [Key]
    public int SanPhamId { get; set; }

    [Key]
    public int SizeId { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DonGia { get; set; }

    [ForeignKey("HoaDonId")]
    [InverseProperty("CthoaDons")]
    public virtual HoaDon HoaDon { get; set; } = null!;

    [ForeignKey("SanPhamId")]
    [InverseProperty("CthoaDons")]
    public virtual SanPham SanPham { get; set; } = null!;

    [ForeignKey("SizeId")]
    [InverseProperty("CthoaDons")]
    public virtual Ctsize Size { get; set; } = null!;
}
