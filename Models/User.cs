using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("User")]
[Index("Username", Name = "UQ__User__536C85E4699D81D4", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(255)]
    public string? HoTen { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("SDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    [StringLength(500)]
    public string? DiaChi { get; set; }

    public int? VaiTro { get; set; }

    public int? TrangThai { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    [InverseProperty("User")]
    public virtual ICollection<CtgioHang> CtgioHangs { get; set; } = new List<CtgioHang>();

    [InverseProperty("User")]
    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    [InverseProperty("User")]
    public virtual ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();
}
