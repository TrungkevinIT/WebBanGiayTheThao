using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; //1. QUAN TRỌNG: Thêm cái này để dùng IFormFile
namespace WebBanGiayTheThao.Models;

[Table("SanPham")]
public partial class SanPham
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Required(ErrorMessage ="Tên sản phẩm không được rỗng")]
    public string TenSanPham { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    [Required(ErrorMessage = "Mã kiểu Dáng không được rỗng")]
    public string? MaKieuDang { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Required(ErrorMessage = "Đơn giá không được rỗng")]
    [Range(0, double.MaxValue,ErrorMessage ="đơn giá không được âm")]
    public decimal? DonGia { get; set; }
  
    public string? AnhDaiDien { get; set; }

    [Required(ErrorMessage = "Mô tả không được rỗng")]
    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public int? ThuongHieuId { get; set; }

    public int? LoaiSanPhamId { get; set; }

    // 1. Hứng file ảnh đại diện từ Form (1 file)
    [NotMapped]
    [Required(ErrorMessage = "Ảnh không được rỗng")]
    public IFormFile? ImageFile { get; set; }

    // 2. Hứng danh sách ảnh phụ từ Form (Nhiều file)
    [NotMapped]
    [Required(ErrorMessage = "Danh sách ảnh không được rỗng")]
    public List<IFormFile>? ListAnhPhu { get; set; }

    // 3. Hứng danh sách Size nhập từ bảng (Dùng luôn class Ctsize có sẵn)
    // Lưu ý: Ta tạo list mới để binding dữ liệu cho dễ, tránh đụng vào ICollection ảo ở dưới
    [NotMapped]
    public List<Ctsize> ChiTietSizeNhap { get; set; } = new List<Ctsize>();

    [InverseProperty("SanPham")]
    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    [InverseProperty("SanPham")]
    public virtual ICollection<Ctanh> Ctanhs { get; set; } = new List<Ctanh>();

    [InverseProperty("SanPham")]
    public virtual ICollection<CtgioHang> CtgioHangs { get; set; } = new List<CtgioHang>();

    [InverseProperty("SanPham")]
    public virtual ICollection<CthoaDon> CthoaDons { get; set; } = new List<CthoaDon>();

    [InverseProperty("SanPham")]
    public virtual ICollection<Ctsize> Ctsizes { get; set; } = new List<Ctsize>();

    [ForeignKey("LoaiSanPhamId")]
    [InverseProperty("SanPhams")]
    public virtual LoaiSanPham? LoaiSanPham { get; set; }

    [ForeignKey("ThuongHieuId")]
    [InverseProperty("SanPhams")]
    public virtual ThuongHieu? ThuongHieu { get; set; }
}
