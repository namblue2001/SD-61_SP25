using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StyleTee.Models;

namespace StyleTee.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<ChatLieu> ChatLieu { get; set; }
    public DbSet<DanhMuc> DanhMuc { get; set; }
    public DbSet<DiaChi> DiaChi { get; set; }
    public DbSet<HinhAnh> HinhAnh { get; set; }
    public DbSet<KichThuoc> KichThuoc { get; set; }
    public DbSet<KieuDang> KieuDang { get; set; }
    public DbSet<MauSac> MauSac { get; set; }
    public DbSet<SanPham> SanPham { get; set; }
    public DbSet<SanPhamChiTiet> SanPhamChiTiet { get; set; }
    public DbSet<TaiKhoan> TaiKhoan { get; set; }
    public DbSet<ThuongHieu> ThuongHieu { get; set; }
    public DbSet<XuatXu> XuatXu { get; set; }
    public DbSet<GioHang> GioHang { get; set; }
    public DbSet<GioHangChiTiet> GioHangChiTiet { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
       
        builder.Entity<HinhAnh>().HasOne(h => h.SanPhamChiTiet).WithMany(s => s.HinhAnh).HasForeignKey(p => p.ID_SanPhamChiTiet);
        builder.Entity<DiaChi>().HasOne(d => d.TaiKhoan).WithMany(t => t.DiaChis).HasForeignKey(d => d.ID_TaiKhoan);
        builder.Entity<SanPhamChiTiet>().HasOne(x => x.SanPham).WithMany(p => p.SanPhamChiTiet).HasForeignKey(x => x.ID_SanPham);
        builder.Entity<SanPhamChiTiet>().HasOne(x => x.MauSac).WithMany(p => p.SanPhamChiTiets).HasForeignKey(x => x.ID_Mau);
        builder.Entity<SanPhamChiTiet>().HasOne(x => x.XuatXu).WithMany(p => p.SanPhamChiTiets).HasForeignKey(x => x.ID_XuatXu);
        builder.Entity<SanPhamChiTiet>().HasOne(x => x.ThuongHieu).WithMany(p => p.SanPhamChiTiet).HasForeignKey(x => x.ID_ThuongHieu);
        builder.Entity<SanPhamChiTiet>().HasOne(x => x.KieuDang).WithMany(p => p.SanPhamChiTiet).HasForeignKey(x => x.ID_KieuDang);
        builder.Entity<SanPhamChiTiet>().HasOne(x => x.KichThuoc).WithMany(p => p.SanPhamChiTiet).HasForeignKey(x => x.ID_Size);
        builder.Entity<SanPhamChiTiet>().HasOne(x => x.ChatLieu).WithMany(p => p.SanPhamChiTiet).HasForeignKey(x => x.ID_ChatLieu);
        builder.Entity<SanPham>().HasOne(x => x.DanhMuc).WithMany(p => p.SanPham).HasForeignKey(x => x.ID_DanhMuc);
        builder.Entity<GioHang>().HasOne(x => x.TaiKhoan).WithMany(p => p.GioHang).HasForeignKey(x => x.ID_TaiKhoan);
        builder.Entity<GioHangChiTiet>().HasOne(x => x.SanPhamChiTiet).WithMany(p => p.GioHangChiTiets).HasForeignKey(x => x.ID_SanPhamChiTiet);
        builder.Entity<GioHangChiTiet>().HasOne(x => x.GioHang).WithMany(p => p.GioHangChiTiet).HasForeignKey(x => x.ID_GioHang);
    }
}

