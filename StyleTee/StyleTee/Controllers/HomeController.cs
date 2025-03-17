using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using StyleTee.Data;

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
    
    public IActionResult Index()
    {
        TempData["Trang"] = "Bảng điều khiển";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
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

    public IActionResult SanPham()
    {
        var trangthai = HttpContext.Session.GetString("id_taikhoan");
        TempData["TrangThai"] = trangthai == null ? "Đăng nhập" : "Đăng xuất";
        return View();
    }

    public IActionResult ThanhToan()
    {
        var trangthai = HttpContext.Session.GetString("id_taikhoan");
        TempData["TrangThai"] = trangthai == null ? "Đăng nhập" : "Đăng xuất";
        return View();
    }
    
    public IActionResult ChiTietSanPham()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

