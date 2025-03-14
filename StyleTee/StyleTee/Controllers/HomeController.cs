using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;

namespace StyleTee.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult GioHang()
    {
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

