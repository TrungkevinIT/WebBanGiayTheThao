using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("UserVoucher")]
public partial class UserVoucher
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? VoucherId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DonToiThieu { get; set; }

    public bool? DaSuDung { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayNhan { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserVouchers")]
    public virtual User? User { get; set; }

    [ForeignKey("VoucherId")]
    [InverseProperty("UserVouchers")]
    public virtual Voucher? Voucher { get; set; }
}
