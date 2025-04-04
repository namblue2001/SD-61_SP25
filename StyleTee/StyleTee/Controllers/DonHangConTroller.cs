using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;
using System.Security.Claims;

namespace StyleTee.Controllers
{
    [Authorize]
    public class DonHangConTroller : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonHangConTroller(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách đơn hàng của người dùng
        public async Task<IActionResult> MyOrders()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _context.DonHang
                .Include(o => o.ChiTietDonHang)
                    .ThenInclude(od => od.SanPhamChiTiet)
                .Where(o => o.ID_TaiKhoan == userId)
                .OrderByDescending(o => o.ngayDatHang)
                .ToListAsync();

            return View(orders);
        }

        // Hiển thị chi tiết đơn hàng
        public async Task<IActionResult> OrderDetails(Guid id)
        {
            var order = await _context.DonHang
                .Include(o => o.ChiTietDonHang)
                    .ThenInclude(od => od.SanPhamChiTiet)
                .Include(o => o.LichSuDonHang)
                .FirstOrDefaultAsync(o => o.ID_DonHang == id);

            if (order == null)
            {
                return NotFound();
            }

            // Kiểm tra xem người dùng có quyền xem đơn hàng này không
            if (order.ID_TaiKhoan != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return Forbid();
            }

            return View(order);
        }

        // Tạo đơn hàng mới từ giỏ hàng
        [HttpPost]
        public async Task<IActionResult> CreateOrder(string shippingAddress, string paymentMethod)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = await _context.GioHang
                .Include(c => c.GioHangChiTiet)
                    .ThenInclude(ci => ci.SanPhamChiTiet)
                .FirstOrDefaultAsync(c => c.ID_TaiKhoan == userId);

            if (cart == null || !cart.GioHangChiTiet.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var order = new DonHang
            {
                ID_TaiKhoan = userId,
                ngayDatHang = DateTime.Now,
                //diaChiVanChuyen = shippingAddress,
                //phuongThucThanhToan = paymentMethod,
                trangThaiThanhToan = "Pending",
                trangThaiDonHang = "Pending",
                tongTien = cart.GioHangChiTiet.Sum(ci => ci.soLuong * ci.SanPhamChiTiet.giaBan)
            };

            _context.DonHang.Add(order);
            await _context.SaveChangesAsync();

            // Tạo chi tiết đơn hàng từ giỏ hàng
            foreach (var cartItem in cart.GioHangChiTiet)
            {
                var orderDetail = new ChiTietDonHang
                {
                    ID_DonHang = order.ID_DonHang,
                    ID_SanPhamChiTiet =cartItem.SanPhamChiTiet.ID_SanPhamChiTiet,
                    soLuong = cartItem.soLuong,
                    donGia = cartItem.SanPhamChiTiet.giaBan,
                    tongTien = cartItem.soLuong * cartItem.SanPhamChiTiet.giaBan
                };

                _context.ChiTietDonHang.Add(orderDetail);
            }

            // Tạo lịch sử trạng thái đơn hàng
            var statusHistory = new LichSuDonHang
            {
                ID_DonHang = order.ID_DonHang,
                trangThai = "Pending",
                ngayDatHang = DateTime.Now,
                ghiChu = "Đơn hàng được tạo"
            };

            _context.LichSuDonHang.Add(statusHistory);

            // Xóa giỏ hàng
            _context.GioHangChiTiet.RemoveRange(cart.GioHangChiTiet);
            _context.GioHang.Remove(cart);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderDetails), new { id = order.ChiTietDonHang });
        }

        // Hủy đơn hàng
        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.DonHang.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.ID_TaiKhoan != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return Forbid();
            }

            if (order.trangThaiDonHang != "Pending")
            {
                return BadRequest("Không thể hủy đơn hàng ở trạng thái này");
            }

            order.trangThaiDonHang = "Cancelled";
            //order.phuongThucThanhToan = "Cancelled";

            var statusHistory = new LichSuDonHang
            {
                ID_DonHang = order.ID_DonHang,
                trangThai = "Cancelled",
                ngayDatHang = DateTime.Now,
                ghiChu = "Đơn hàng bị hủy bởi người dùng"
            };

            _context.LichSuDonHang.Add(statusHistory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderDetails), new { id = order.ID_DonHang });
        }
    }
} 