using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("ThuongHieu")]
public partial class ThuongHieu
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string TenThuongHieu { get; set; } = null!;

    [StringLength(255)]
    public string XuatXu { get; set; } = null!;

    public int? TrangThai { get; set; }

    [InverseProperty("ThuongHieu")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
