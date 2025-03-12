using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using StyleTee.Data;
using StyleTee.Models;
using StyleTee.Service;
using StyleTee.Service.IService;

namespace StyleTee.Controllers
{
    public class AccessController : Controller
    {
        private readonly ITaiKhoanService _tkservice;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public AccessController(ITaiKhoanService tkservice, ApplicationDbContext context, IConfiguration config)
        {
            _tkservice = tkservice;
            _context = context;
            _config = config;
        }

        public IActionResult DangKy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DangKy(string hoTen, string email, string soDienThoai, DateTime ngaySinh, string gioiTinh, string taiKhoan, string matKhau)

        {
            //var userexist = _context.TaiKhoan.FirstOrDefault(x=>x.taiKhoan ==  taiKhoan);
            //if (userexist == null)
            //{
            //    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại";
            //}
            var taikhoan = new TaiKhoan()
            {
                hoTen = hoTen,
                email = email,
                soDienThoai = soDienThoai,
                ngaySinh = ngaySinh,
                gioiTinh = gioiTinh,
                taiKhoan = taiKhoan,
                matKhau = matKhau
            };
            try
            {
                taikhoan.ID_TaiKhoan = Guid.NewGuid();
                taikhoan.trangThai = "Hoạt động";
                taikhoan.tenVaiTro = "Khách hàng"; 
                _context.TaiKhoan.Add(taikhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction("DangNhap", "Access");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["lỗi"] = "Thông tin bạn nhập không đúng. Vui lòng kiểm tra lại";
                return View(taikhoan);
            }
        }
        public IActionResult DangNhap(string taiKhoan, string matKhau)
        {
            if (HttpContext.Request.Method == "POST")
            {
                // Kiểm tra tài khoản và mật khẩu
                if (IsValidUser(taiKhoan, matKhau))
                {
                    // Đăng nhập thành công
                    ViewData["SuccessMessage"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index", "Home");
                }

                // Thêm thông báo lỗi
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không hợp lệ.");
                ViewData["ShowError"] = true;
            }

            return View(); // Trả về view
        }

        // Hàm kiểm tra tài khoản và mật khẩu
        private bool IsValidUser(string taiKhoan, string matKhau)
        {
            var taiKhoanDb = _context.TaiKhoan.FirstOrDefault(u => u.taiKhoan == taiKhoan);
            if (taiKhoanDb != null)
            {
                return taiKhoanDb.matKhau == matKhau;
            }
            return false;
        }

        // Hiển thị form quên mật khẩu
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // Xử lý quên mật khẩu
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _context.TaiKhoan.FirstOrDefaultAsync(u => u.email == email);
            if (user == null)
            {
                ViewBag.Message = "Email không tồn tại!";
                return View();
            }

            // Giả sử mật khẩu chưa hash
            string oldPassword = user.matKhau;

            await SendEmail(email, oldPassword);

            ViewBag.Message = "Mật khẩu đã được gửi vào email của bạn!";
            return View();
        }


        // Gửi email
        private async Task SendEmail(string email, string oldPassword)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Style Tee", _config["Smtp:Username"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Mật khẩu tài khoản của bạn";
            message.Body = new TextPart("html")
            {
                Text = $"<p>Mật khẩu của bạn là: <strong>{oldPassword}</strong></p>"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]), false);
            await client.AuthenticateAsync(_config["Smtp:Username"], _config["Smtp:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

    }
}

