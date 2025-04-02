using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StyleTee.Models;
using StyleTee.Models.PhieuGiamGiaVaKhuyenMai;

namespace StyleTee.Data;

public class ApplicationDbContext : IdentityDbContext
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
    public DbSet<PhieuGiamGia> PhieuGiamGia { get; set; }
    public DbSet<KhuyenMai> KhuyenMai { get; set; }
    public DbSet<DonHang> DonHang { get; set; }
    public DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
    public DbSet<LichSuDonHang> LichSuDonHang { get; set; }
    
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

        builder.Entity<PhieuGiamGia>().HasIndex(k => k.TenPhieuGiamGia).IsUnique().HasDatabaseName("UQ_TenPhieuGiamGia");
        builder.Entity<KhuyenMai>().HasIndex(k => new { k.TyLeKhuyenMai, k.NgayBatDau }).IsUnique().HasDatabaseName("UQ_TyLeKhuyenMai");
        builder.Entity<KhuyenMai>().HasOne(x => x.SanPhamChiTiet).WithMany().HasForeignKey(x => x.ID_SanPhamChiTiet);


        builder.Entity<ChiTietDonHang>().HasOne(x => x.DonHang).WithMany(p=>p.ChiTietDonHang).HasForeignKey(x => x.ID_DonHang);
        builder.Entity<ChiTietDonHang>().HasOne(x => x.SanPhamChiTiet).WithMany(p=>p.DonHangChiTiet).HasForeignKey(x => x.ID_SanPhamChiTiet);
        builder.Entity<DonHang>().HasOne(x => x.TaiKhoan).WithMany(p=>p.DonHang).HasForeignKey(x => x.ID_TaiKhoan);
        builder.Entity<LichSuDonHang>().HasOne(x => x.DonHang).WithMany(p=>p.LichSuDonHang).HasForeignKey(x => x.ID_DonHang);
      
    }
}

