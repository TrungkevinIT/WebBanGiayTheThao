using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("Voucher")]
public partial class Voucher
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? MaCode { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? GiaTriDonToiThieu { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? GiaTriGiam { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayBatDau { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayKetThuc { get; set; }

    public int? SoLuong { get; set; }

    public int? TrangThai { get; set; }

    [InverseProperty("Voucher")]
    public virtual ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();
}
