using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("CTAnh")]
public partial class Ctanh
{
    [Key]
    public int Id { get; set; }

    public string? LinkAnh { get; set; }

    public int? SanPhamId { get; set; }

    [ForeignKey("SanPhamId")]
    [InverseProperty("Ctanhs")]
    public virtual SanPham? SanPham { get; set; }
}
