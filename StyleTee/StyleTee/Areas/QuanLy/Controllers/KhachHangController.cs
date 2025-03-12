using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using StyleTee.Data;
using StyleTee.Models;

namespace StyleTee.Areas.QuanLy.Controllers
{
    [Area("QuanLy")]
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách khách hàng
        public async Task<IActionResult> Index(string search, string sortBy = "hoTen", string sortOrder = "asc", int page = 1, int pageSize = 10)
        {
            var customers = _context.TaiKhoan.Where(c => c.tenVaiTro == "Khách hàng");

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(c =>
                    c.hoTen.Contains(search) ||
                    c.email.Contains(search) ||
                    c.soDienThoai.Contains(search));
            }

            // Sắp xếp
            customers = sortBy switch
            {
                "hoTen" => sortOrder == "asc" ? customers.OrderBy(c => c.hoTen) : customers.OrderByDescending(c => c.hoTen),
                "ngaySinh" => sortOrder == "asc" ? customers.OrderBy(c => c.ngaySinh) : customers.OrderByDescending(c => c.ngaySinh),
                "trangThai" => sortOrder == "asc" ? customers.OrderBy(c => c.trangThai) : customers.OrderByDescending(c => c.trangThai),
                _ => customers.OrderBy(c => c.hoTen)
            };

            // Phân trang
            int totalItems = await customers.CountAsync();
            var pagedCustomers = await customers.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.Page = page;
            ViewBag.TotalPages = (totalItems + pageSize - 1) / pageSize;
            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;

            return View(pagedCustomers);
        }

        // Hiển thị form thêm khách hàng
        public IActionResult Create()
        {
            return View();
        }

        // Thêm khách hàng mới
        [HttpPost]
        public async Task<IActionResult> Create(TaiKhoan customer)
        {
            if (ModelState.IsValid)
            {
                customer.ID_TaiKhoan = Guid.NewGuid();
                customer.tenVaiTro = "Khách hàng";
                _context.TaiKhoan.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Hiển thị form chỉnh sửa khách hàng
        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await _context.TaiKhoan.FindAsync(id);
            if (customer == null || customer.tenVaiTro != "Khách hàng")
            {
                return NotFound();
            }
            return View(customer);
        }

        // Cập nhật thông tin khách hàng
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, TaiKhoan customer)
        {
            if (id != customer.ID_TaiKhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingCustomer = await _context.TaiKhoan.FindAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }

                existingCustomer.hoTen = customer.hoTen;
                existingCustomer.email = customer.email;
                existingCustomer.soDienThoai = customer.soDienThoai;
                existingCustomer.gioiTinh = customer.gioiTinh;
                existingCustomer.ngaySinh = customer.ngaySinh;
                existingCustomer.trangThai = customer.trangThai;

                _context.TaiKhoan.Update(existingCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Chuyển đổi trạng thái khách hàng
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var customer = await _context.TaiKhoan.FindAsync(id);
            if (customer == null || customer.tenVaiTro != "Khách hàng")
            {
                return NotFound();
            }

            customer.trangThai = customer.trangThai == "Hoạt động" ? "Ngừng hoạt động" : "Hoạt động";
            _context.TaiKhoan.Update(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Xóa khách hàng
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _context.TaiKhoan.FindAsync(id);
            if (customer == null || customer.tenVaiTro != "Khách hàng")
            {
                return NotFound();
            }

            _context.TaiKhoan.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Gửi email chăm sóc khách hàng
        public async Task<IActionResult> SendBirthdayEmail(Guid id)
        {
            var customer = await _context.TaiKhoan.FindAsync(id);
            if (customer == null || customer.tenVaiTro != "Khách hàng")
            {
                return NotFound();
            }

            if (customer.ngaySinh.Date != DateTime.Today)
            {
                TempData["Message"] = "Hôm nay không phải sinh nhật khách hàng.";
                return RedirectToAction("Index");
            }

            await SendEmail(customer.email, "Chúc mừng sinh nhật!",
                $"<p>Chúc mừng sinh nhật {customer.hoTen}! 🎉<br>Chúc bạn có một ngày tuyệt vời!</p>");

            TempData["Message"] = "Email chúc mừng sinh nhật đã được gửi.";
            return RedirectToAction("Index");
        }

        // Hàm gửi email
        private async Task SendEmail(string toEmail, string subject, string content)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Style Tee", "your-email@example.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = content };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.example.com", 587, false);
            await client.AuthenticateAsync("your-email@example.com", "your-email-password");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
