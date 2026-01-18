using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("HoaDon")]
public partial class HoaDon
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayDat { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TongTien { get; set; }

    [Required(ErrorMessage ="Vui Lòng nhập họ và tên người nhận.")]
    [StringLength(255)]
    public string? HoTenNguoiNhan { get; set; }

    [Required(ErrorMessage = "Vui Lòng nhập địa chỉ người nhận.")]
    [StringLength(500)]
    public string? DiaChiNhan { get; set; }

    [Column("SDTNhan")]
    [StringLength(10)]
    [Unicode(false)]
    [Required(ErrorMessage = "Vui Lòng nhập số điện thoại người nhận.")]
    public string? Sdtnhan { get; set; }

    public string? GhiChu { get; set; }

    public int? PhuongThucThanhToan { get; set; }

    public int? TrangThai { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MaVoucher { get; set; }

    [InverseProperty("HoaDon")]
    public virtual ICollection<CthoaDon> CthoaDons { get; set; } = new List<CthoaDon>();

    [ForeignKey("UserId")]
    [InverseProperty("HoaDons")]
    public virtual User User { get; set; } = null!;
}
