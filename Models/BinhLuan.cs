using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("BinhLuan")]
public partial class BinhLuan
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SanPhamId { get; set; }

    public string? NoiDung { get; set; }

    public int? Sao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("SanPhamId")]
    [InverseProperty("BinhLuans")]
    public virtual SanPham SanPham { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("BinhLuans")]
    public virtual User User { get; set; } = null!;
}
