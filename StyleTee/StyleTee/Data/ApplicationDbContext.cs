using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StyleTee.Models;

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
    public DbSet<MauSac> MauSac { get; set; }
    public DbSet<SanPham> SanPham { get; set; }
    public DbSet<SanPhamChiTiet> SanPhamChiTiet { get; set; }
    public DbSet<TaiKhoan> TaiKhoan { get; set; }
    public DbSet<ThuongHieu> ThuongHieu { get; set; }
    public DbSet<VaiTro> VaiTro { get; set; }
    public DbSet<XuatXu> XuatXu { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<TaiKhoan>().HasOne(x => x.VaiTro).WithOne(y => y.TaiKhoan).HasForeignKey<TaiKhoan>(p => p.ID_VaiTro);

    }
}

