using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;
using System.Security.Claims;

namespace StyleTee.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách đơn hàng của người dùng
        public async Task<IActionResult> MyOrders()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.SanPham)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // Hiển thị chi tiết đơn hàng
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.SanPham)
                .Include(o => o.OrderStatusHistories)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            // Kiểm tra xem người dùng có quyền xem đơn hàng này không
            if (order.UserId != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
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

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShippingAddress = shippingAddress,
                PaymentMethod = paymentMethod,
                PaymentStatus = "Pending",
                OrderStatus = "Pending",
                TotalAmount = cart.GioHangChiTiet.Sum(ci => ci.soLuong * ci.SanPhamChiTiet.giaBan)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Tạo chi tiết đơn hàng từ giỏ hàng
            foreach (var cartItem in cart.GioHangChiTiet)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = (int)cartItem.SanPhamChiTiet.ID_SanPhamChiTiet.GetHashCode(),
                    Quantity = cartItem.soLuong,
                    UnitPrice = cartItem.SanPhamChiTiet.giaBan,
                    Subtotal = cartItem.soLuong * cartItem.SanPhamChiTiet.giaBan
                };

                _context.OrderDetails.Add(orderDetail);
            }

            // Tạo lịch sử trạng thái đơn hàng
            var statusHistory = new OrderStatusHistory
            {
                OrderId = order.OrderId,
                Status = "Pending",
                StatusDate = DateTime.Now,
                Notes = "Đơn hàng được tạo"
            };

            _context.OrderStatusHistories.Add(statusHistory);

            // Xóa giỏ hàng
            _context.GioHangChiTiet.RemoveRange(cart.GioHangChiTiet);
            _context.GioHang.Remove(cart);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderDetails), new { id = order.OrderId });
        }

        // Hủy đơn hàng
        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.UserId != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return Forbid();
            }

            if (order.OrderStatus != "Pending")
            {
                return BadRequest("Không thể hủy đơn hàng ở trạng thái này");
            }

            order.OrderStatus = "Cancelled";
            order.PaymentStatus = "Cancelled";

            var statusHistory = new OrderStatusHistory
            {
                OrderId = order.OrderId,
                Status = "Cancelled",
                StatusDate = DateTime.Now,
                Notes = "Đơn hàng bị hủy bởi người dùng"
            };

            _context.OrderStatusHistories.Add(statusHistory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderDetails), new { id = order.OrderId });
        }
    }
} 