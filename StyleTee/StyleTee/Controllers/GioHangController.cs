using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StyleTee.Models;

public class GioHangController : Controller
{
    private const string CartSessionKey = "GioHang";

    // Hiển thị giỏ hàng
    public IActionResult Index()
    {
        var gioHang = GetGioHangFromSession();
        return View(gioHang);
    }

    // Thêm sản phẩm vào giỏ hàng
    [HttpPost]
    public IActionResult AddToCart(Guid idSanPhamChiTiet, string tenSanPham, string anhDaiDien, decimal donGia, int soLuong)
    {
        var gioHang = GetGioHangFromSession();

        var chiTiet = gioHang.ChiTietGioHang.FirstOrDefault(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);
        if (chiTiet != null)
        {
            chiTiet.SoLuong += soLuong;
        }
        else
        {
            gioHang.ChiTietGioHang.Add(new GioHangChiTietViewModel
            {
                ID_SanPhamChiTiet = idSanPhamChiTiet,
                TenSanPham = tenSanPham,
                AnhDaiDien = anhDaiDien,
                DonGia = donGia,
                SoLuong = soLuong
            });
        }

        SaveGioHangToSession(gioHang);
        return RedirectToAction("Index");
    }

    // Xóa sản phẩm khỏi giỏ hàng
    [HttpPost]
    public IActionResult RemoveFromCart(Guid idSanPhamChiTiet)
    {
        var gioHang = GetGioHangFromSession();
        gioHang.ChiTietGioHang.RemoveAll(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);
        SaveGioHangToSession(gioHang);

        return RedirectToAction("Index");
    }

    // Lấy giỏ hàng từ Session
    private GioHangViewModel GetGioHangFromSession()
    {
        var gioHangJson = HttpContext.Session.GetString(CartSessionKey);
        return gioHangJson == null ? new GioHangViewModel() : JsonConvert.DeserializeObject<GioHangViewModel>(gioHangJson);
    }

    // Lưu giỏ hàng vào Session
    private void SaveGioHangToSession(GioHangViewModel gioHang)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonConvert.SerializeObject(gioHang));
    }
}
