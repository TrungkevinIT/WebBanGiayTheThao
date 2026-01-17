using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Data;

public partial class QuanLyWebBanGiayContext : DbContext
{
    public QuanLyWebBanGiayContext()
    {
    }

    public QuanLyWebBanGiayContext(DbContextOptions<QuanLyWebBanGiayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BinhLuan> BinhLuans { get; set; }

    public virtual DbSet<Ctanh> Ctanhs { get; set; }

    public virtual DbSet<CtgioHang> CtgioHangs { get; set; }

    public virtual DbSet<CthoaDon> CthoaDons { get; set; }

    public virtual DbSet<Ctsize> Ctsizes { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<SlideShow> SlideShows { get; set; }

    public virtual DbSet<ThuongHieu> ThuongHieus { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserVoucher> UserVouchers { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<WebSetting> WebSettings { get; set; }

    public  virtual DbSet<LienHe> LienHes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BinhLuan__3214EC07CEA3D11E");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SanPham).WithMany(p => p.BinhLuans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BinhLuan__SanPha__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.BinhLuans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BinhLuan__UserId__5FB337D6");
        });

        modelBuilder.Entity<Ctanh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CTAnh__3214EC07E170B2DF");

            entity.HasOne(d => d.SanPham).WithMany(p => p.Ctanhs).HasConstraintName("FK__CTAnh__SanPhamId__4CA06362");
        });

        modelBuilder.Entity<CtgioHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CTGioHan__3214EC07B72D9525");

            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.SanPham).WithMany(p => p.CtgioHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTGioHang__SanPh__5AEE82B9");

            entity.HasOne(d => d.Size).WithMany(p => p.CtgioHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTGioHang__SizeI__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.CtgioHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTGioHang__UserI__59FA5E80");
        });

        modelBuilder.Entity<CthoaDon>(entity =>
        {
            entity.HasKey(e => new { e.HoaDonId, e.SanPhamId, e.SizeId }).HasName("PK__CTHoaDon__4284F3BD8B941929");

            entity.HasOne(d => d.HoaDon).WithMany(p => p.CthoaDons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHoaDon__HoaDon__6754599E");

            entity.HasOne(d => d.SanPham).WithMany(p => p.CthoaDons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHoaDon__SanPha__68487DD7");

            entity.HasOne(d => d.Size).WithMany(p => p.CthoaDons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHoaDon__SizeId__693CA210");
        });

        modelBuilder.Entity<Ctsize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CTSize__3214EC07E64E7E18");

            entity.Property(e => e.SoLuongTon).HasDefaultValue(0);

            entity.HasOne(d => d.SanPham).WithMany(p => p.Ctsizes).HasConstraintName("FK__CTSize__SanPhamI__5070F446");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaDon__3214EC079B9FB4B5");

            entity.Property(e => e.NgayDat).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.HoaDons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__UserId__6477ECF3");
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoaiSanP__3214EC07B04E48E2");

            entity.Property(e => e.TrangThai).HasDefaultValue(1);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SanPham__3214EC07B8D26B72");

            entity.Property(e => e.TrangThai).HasDefaultValue(1);

            entity.HasOne(d => d.LoaiSanPham).WithMany(p => p.SanPhams).HasConstraintName("FK__SanPham__LoaiSan__49C3F6B7");

            entity.HasOne(d => d.ThuongHieu).WithMany(p => p.SanPhams).HasConstraintName("FK__SanPham__ThuongH__48CFD27E");
        });

        modelBuilder.Entity<SlideShow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SlideSho__3214EC07E9487E13");
        });

        modelBuilder.Entity<ThuongHieu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ThuongHi__3214EC076F1C9602");

            entity.Property(e => e.TrangThai).HasDefaultValue(1);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07B77DCB15");

            entity.Property(e => e.TrangThai).HasDefaultValue(1);
            entity.Property(e => e.VaiTro).HasDefaultValue(0);
        });

        modelBuilder.Entity<UserVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserVouc__3214EC0746346C64");

            entity.Property(e => e.DaSuDung).HasDefaultValue(false);
            entity.Property(e => e.NgayNhan).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.UserVouchers).HasConstraintName("FK__UserVouch__UserI__5535A963");

            entity.HasOne(d => d.Voucher).WithMany(p => p.UserVouchers).HasConstraintName("FK__UserVouch__Vouch__5629CD9C");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voucher__3214EC07454989EB");
        });

        modelBuilder.Entity<WebSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WebSetti__3214EC079FCF25EB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
