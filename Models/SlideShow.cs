using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("SlideShow")]
public partial class SlideShow
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    [StringLength(255)]
    public string? Link { get; set; }
}
