using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("CTGioHang")]
public partial class CtgioHang
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SanPhamId { get; set; }

    public int SizeId { get; set; }

    public int? SoLuong { get; set; }

    [ForeignKey("SanPhamId")]
    [InverseProperty("CtgioHangs")]
    public virtual SanPham SanPham { get; set; } = null!;

    [ForeignKey("SizeId")]
    [InverseProperty("CtgioHangs")]
    public virtual Ctsize Size { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("CtgioHangs")]
    public virtual User User { get; set; } = null!;
}
