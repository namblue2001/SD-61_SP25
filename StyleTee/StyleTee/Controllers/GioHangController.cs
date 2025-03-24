using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;

namespace StyleTee.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GioHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị giỏ hàng
        public async Task<IActionResult> Index()
        {
            var userId = GetUserIdFromSession();
            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                .ThenInclude(ct => ct.SanPhamChiTiet)
                .ThenInclude(sp => sp.SanPham)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == userId);

            var gioHangViewModel = new GioHangViewModel
            {
                ChiTietGioHang = gioHang?.GioHangChiTiet?.Select(ct => new GioHangChiTietViewModel
                {
                    ID_SanPhamChiTiet = ct.ID_SanPhamChiTiet,
                    TenSanPham = ct.SanPhamChiTiet.SanPham.tenSanPham,
                    AnhDaiDien = ct.SanPhamChiTiet.anhDaiDien,
                    DonGia = ct.donGia,
                    SoLuong = ct.soLuong
                }).ToList() ?? new List<GioHangChiTietViewModel>()
            };

            return View(gioHangViewModel);
        }

        // Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid idSanPhamChiTiet, int soLuong)
        {
            var userId = GetUserIdFromSession();
            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == userId);

            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    ID_GioHang = Guid.NewGuid(),
                    ID_TaiKhoan = userId,
                    ngayTao = DateTime.UtcNow,
                    GioHangChiTiet = new List<GioHangChiTiet>()
                };
                _context.GioHang.Add(gioHang);
            }

            var spChiTiet = await _context.SanPhamChiTiet.FindAsync(idSanPhamChiTiet);
            if (spChiTiet == null || spChiTiet.soLuongTon < soLuong)
            {
                TempData["Error"] = "Sản phẩm không đủ số lượng!";
                return RedirectToAction("Index");
            }

            var chiTiet = gioHang.GioHangChiTiet.FirstOrDefault(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);
            if (chiTiet != null)
            {
                chiTiet.soLuong += soLuong;
            }
            else
            {
                gioHang.GioHangChiTiet.Add(new GioHangChiTiet
                {
                    ID_GioHangChiTiet = Guid.NewGuid(),
                    ID_GioHang = gioHang.ID_GioHang,
                    ID_SanPhamChiTiet = idSanPhamChiTiet,
                    soLuong = soLuong,
                    donGia = spChiTiet.giaBan
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid idSanPhamChiTiet)
        {
            var userId = GetUserIdFromSession();
            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == userId);

            if (gioHang == null) return RedirectToAction("Index");

            var chiTiet = gioHang.GioHangChiTiet.FirstOrDefault(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);
            if (chiTiet != null)
            {
                gioHang.GioHangChiTiet.Remove(chiTiet);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private Guid GetUserIdFromSession()
        {
            return Guid.TryParse(HttpContext.Session.GetString("UserId"), out var userId) ? userId : Guid.Empty;
        }
    }
}
