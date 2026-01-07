using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("SanPham")]
public partial class SanPham
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string TenSanPham { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? MaKieuDang { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DonGia { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public int? ThuongHieuId { get; set; }

    public int? LoaiSanPhamId { get; set; }

    [InverseProperty("SanPham")]
    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    [InverseProperty("SanPham")]
    public virtual ICollection<Ctanh> Ctanhs { get; set; } = new List<Ctanh>();

    [InverseProperty("SanPham")]
    public virtual ICollection<CtgioHang> CtgioHangs { get; set; } = new List<CtgioHang>();

    [InverseProperty("SanPham")]
    public virtual ICollection<CthoaDon> CthoaDons { get; set; } = new List<CthoaDon>();

    [InverseProperty("SanPham")]
    public virtual ICollection<Ctsize> Ctsizes { get; set; } = new List<Ctsize>();

    [ForeignKey("LoaiSanPhamId")]
    [InverseProperty("SanPhams")]
    public virtual LoaiSanPham? LoaiSanPham { get; set; }

    [ForeignKey("ThuongHieuId")]
    [InverseProperty("SanPhams")]
    public virtual ThuongHieu? ThuongHieu { get; set; }
}
