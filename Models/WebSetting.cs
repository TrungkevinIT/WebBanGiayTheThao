using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiayTheThao.Models;

[Table("WebSetting")]
public partial class WebSetting
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? Logo { get; set; }

    [StringLength(50)]
    public string? Hotline { get; set; }

    [StringLength(100)]
    public string? EmailLienHe { get; set; }
}
