using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using StyleTee.Data;
using Microsoft.EntityFrameworkCore;

namespace StyleTee.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult HoSoQuanLy()
    {
        var id_taikhoan = HttpContext.Session.GetString("id_taikhoan");
        var quanly = _context.TaiKhoan.FirstOrDefault(a => a.ID_TaiKhoan == Guid.Parse(id_taikhoan));
        TempData["Trang"] = "Hồ sơ";
        return View(quanly);
    }
    [HttpPost]
    public IActionResult UpdateProfile(TaiKhoan model)
    {
        var id_taikhoan = HttpContext.Session.GetString("id_taikhoan");

        if (string.IsNullOrEmpty(id_taikhoan))
        {
            return RedirectToAction("Login", "Auth");
        }

        var taiKhoan = _context.TaiKhoan.FirstOrDefault(a => a.ID_TaiKhoan == model.ID_TaiKhoan);

        if (taiKhoan == null)
        {
            return NotFound();
        }

        // Cập nhật thông tin
        taiKhoan.hoTen = model.hoTen;
        taiKhoan.email = model.email;
        taiKhoan.soDienThoai = model.soDienThoai;
        taiKhoan.gioiTinh = model.gioiTinh;
        taiKhoan.ngaySinh = model.ngaySinh;

        _context.TaiKhoan.Update(taiKhoan);
        _context.SaveChanges();

        TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
        return RedirectToAction("HoSoQuanLy");
    }

    public IActionResult Index()
    {
        TempData["Trang"] = "Bảng điều khiển";
        return View();
    }

    public IActionResult SanPham()
    {
        try
        {
            var products = _context.SanPham
                .Include(p => p.DanhMuc)
                .Where(p => p.trangThai == "Hoạt động" && p.DanhMuc != null)
                .ToList();

            if (products == null || !products.Any())
            {
                products = new List<SanPham>();
            }

            return View(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading products in Privacy action");
            return View(new List<SanPham>());
        }
    }

    public IActionResult NhanVien()
    {
        TempData["Trang"] = "Xem báo cáo hiệu suất";
        return View(); 
    }
  
    public IActionResult GioHang()
    {
        var trangthai = HttpContext.Session.GetString("id_taikhoan");
        TempData["TrangThai"] = trangthai == null ? "Đăng nhập" : "Đăng xuất";
        return View();
    }

    //public IActionResult SanPham()
    //{
    //    var trangthai = HttpContext.Session.GetString("id_taikhoan");
    //    TempData["TrangThai"] = trangthai == null ? "Đăng nhập" : "Đăng xuất";
    //    return View();
    //}

    public IActionResult ThanhToan()
    {
        var trangthai = HttpContext.Session.GetString("id_taikhoan");
        TempData["TrangThai"] = trangthai == null ? "Đăng nhập" : "Đăng xuất";
        return View();
    }
    
    public async Task<IActionResult> ChiTietSanPham(Guid id)
    {
        var sanPham = await _context.SanPham
            .Include(s => s.DanhMuc)
            .Include(s => s.SanPhamChiTiet)
                .ThenInclude(spct => spct.MauSac)
            .Include(s => s.SanPhamChiTiet)
                .ThenInclude(spct => spct.KichThuoc)
            .Include(s => s.SanPhamChiTiet)
                .ThenInclude(spct => spct.ChatLieu)
            .Include(s => s.SanPhamChiTiet)
                .ThenInclude(spct => spct.KieuDang)
            .Include(s => s.SanPhamChiTiet)
                .ThenInclude(spct => spct.ThuongHieu)
            .Include(s => s.SanPhamChiTiet)
                .ThenInclude(spct => spct.XuatXu)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ID_SanPham == id);

        if (sanPham == null)
        {
            return NotFound();
        }

        // Get popular products (for now, just get 6 random products excluding the current one)
        var popularProducts = await _context.SanPham
            .Include(s => s.DanhMuc)
            .Where(s => s.ID_SanPham != id && s.trangThai == "Hoạt động")
            .Take(6)
            .ToListAsync();

        ViewBag.PopularProducts = popularProducts;

        if (sanPham.SanPhamChiTiet != null)
        {
            _logger.LogInformation($"Số lượng chi tiết sản phẩm: {sanPham.SanPhamChiTiet.Count}");
            _logger.LogInformation($"Số lượng màu sắc: {sanPham.SanPhamChiTiet.Count(x => x.MauSac != null)}");
            _logger.LogInformation($"Số lượng kích thước: {sanPham.SanPhamChiTiet.Count(x => x.KichThuoc != null)}");
        }

        return View(sanPham);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

