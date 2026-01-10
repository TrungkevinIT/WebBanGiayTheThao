using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("SlideShow")]
[Index(nameof(Link), IsUnique = true)]
public partial class SlideShow
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập đường dẫn (Link).")]
    [StringLength(255)]
    public string? Link { get; set; }
}
