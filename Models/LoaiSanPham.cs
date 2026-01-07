using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("LoaiSanPham")]
public partial class LoaiSanPham
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string TenLoai { get; set; } = null!;

    public int? TrangThai { get; set; }

    [InverseProperty("LoaiSanPham")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
