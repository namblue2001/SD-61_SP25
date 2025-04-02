using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StyleTee.Data;
using StyleTee.Models;

public class GioHangController : Controller
{
    private readonly ApplicationDbContext _db;

    public GioHangController(ApplicationDbContext db)
    {
        _db = db;
    }

    // 📌 **Hàm lấy ID người dùng đang đăng nhập**
    private Guid? GetUserId()
    {
        if (HttpContext.Session.GetString("id_taikhoan") != null)
        {
            return Guid.Parse(HttpContext.Session.GetString("id_taikhoan"));
        }
        return null;
    }

    // 📌 **Hiển thị giỏ hàng**
    public IActionResult Index()
    {
        var userId = GetUserId();
        if (userId == null) return RedirectToAction("DangNhap", "Access");

        var gioHang = _db.GioHang
            .Where(g => g.ID_TaiKhoan == userId)
            .SelectMany(g => g.GioHangChiTiet)
            .Select(c => new GioHangChiTietViewModel
            {
                ID_SanPhamChiTiet = c.ID_SanPhamChiTiet,
                TenSanPham = c.SanPhamChiTiet.SanPham.tenSanPham,
                AnhDaiDien = c.SanPhamChiTiet.anhDaiDien,
                DonGia = c.donGia,
                SoLuong = c.soLuong
            }).ToList();

        return View(gioHang);
    }

    // 📌 **Thêm sản phẩm vào giỏ hàng**
    public IActionResult ThemVaoGio(Guid sanPhamChiTietId, int soLuong)
    {
        var userId = GetUserId();
        if (userId == null) return RedirectToAction("DangNhap", "Access");

        var gioHang = _db.GioHang.FirstOrDefault(g => g.ID_TaiKhoan == userId);
        if (gioHang == null)
        {
            gioHang = new GioHang { ID_GioHang = Guid.NewGuid(), ID_TaiKhoan = userId.Value };
            _db.GioHang.Add(gioHang);
            _db.SaveChanges();
        }

        var chiTiet = _db.GioHangChiTiet
            .FirstOrDefault(c => c.ID_GioHang == gioHang.ID_GioHang && c.ID_SanPhamChiTiet == sanPhamChiTietId);

        if (chiTiet != null)
        {
            chiTiet.soLuong += soLuong;
        }
        else
        {
            _db.GioHangChiTiet.Add(new GioHangChiTiet
            {
                ID_GioHangChiTiet = Guid.NewGuid(),
                ID_GioHang = gioHang.ID_GioHang,
                ID_SanPhamChiTiet = sanPhamChiTietId,
                soLuong = soLuong,
                donGia = _db.SanPhamChiTiet.Find(sanPhamChiTietId)?.giaBan ?? 0
            });
        }

        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    // 📌 **Xóa sản phẩm khỏi giỏ hàng**
    public IActionResult XoaKhoiGio(Guid sanPhamChiTietId)
    {
        var userId = GetUserId();
        if (userId == null) return RedirectToAction("Login", "TaiKhoan");

        var gioHang = _db.GioHang.FirstOrDefault(g => g.ID_TaiKhoan == userId);
        if (gioHang != null)
        {
            var chiTiet = _db.GioHangChiTiet
                .FirstOrDefault(c => c.ID_GioHang == gioHang.ID_GioHang && c.ID_SanPhamChiTiet == sanPhamChiTietId);

            if (chiTiet != null)
            {
                _db.GioHangChiTiet.Remove(chiTiet);
                _db.SaveChanges();
            }
        }
        return RedirectToAction("Index");
    }
}
