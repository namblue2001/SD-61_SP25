using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;



namespace StyleTee.Controllers
{
    public class AccessController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public AccessController(ApplicationDbContext context, IConfiguration config)
        {
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
        public IActionResult DangNhap()
        {
            HttpContext.Session.Remove("id_taikhoan");
            return View();
        }
        [HttpPost]
        public IActionResult DangNhap(string taikhoan, string matkhau)
        {
            var taikhoandangnhap = _context.TaiKhoan.FirstOrDefault(a => a.taiKhoan == taikhoan && a.matKhau == matkhau);
            if (taikhoandangnhap != null)
            {
                HttpContext.Session.SetString("id_taikhoan", taikhoandangnhap.ID_TaiKhoan.ToString());
                if (taikhoandangnhap.tenVaiTro == "Quản lý" && taikhoandangnhap.trangThai == "Hoạt động")
                {
                    TempData["Thành công"] = "Chào mừng! Bạn đã đăng nhập thành công.";
                    return RedirectToAction("Index", "Home");
                }
                else if (taikhoandangnhap.tenVaiTro == "Nhân viên" && taikhoandangnhap.trangThai == "Hoạt động")
                {
                    return RedirectToAction("NhanVien", "Home");
                }
                else if (taikhoandangnhap.tenVaiTro == "Khách hàng" && taikhoandangnhap.trangThai == "Hoạt động")
                {
                    return RedirectToAction("Privacy", "Home");
                }
                else if (taikhoandangnhap.trangThai == "Ngừng hoạt động")
                {
                    TempData["Lỗi"] = "Tài khoản đã ngừng hoạt động. Vui lòng liên hệ quản trị viên để biết thêm chi tiết.";
                }
                else
                {
                    TempData["Lỗi"] = "Thông tin bạn nhập không đúng.Vui lòng kiểm tra lại!";
                }
            }
            else
            {
                TempData["Lỗi"] = "Thông tin bạn nhập không đúng.Vui lòng kiểm tra lại!";
            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng nhập email!";
                return View();
            }

            var user = await _context.TaiKhoan.FirstOrDefaultAsync(u => u.email == email);
            if (user == null)
            {
                ViewBag.Error = "Email không tồn tại trong hệ thống!";
                return View();
            }

            // Gửi email chứa mật khẩu cũ
            string subject = "Khôi phục mật khẩu - Shop Online";
            string body = $"Xin chào {user.hoTen},<br><br>Mật khẩu của bạn là: <strong>{user.matKhau}</strong><br><br>Vui lòng đăng nhập và đổi mật khẩu nếu cần!";

            bool isSent = SendEmail(email, subject, body);
            if (isSent)
            {
                ViewBag.Success = "Mật khẩu đã được gửi đến email của bạn!";
            }
            else
            {
                ViewBag.Error = "Có lỗi xảy ra khi gửi email!";
            }

            return View();
        }

        private bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(_config["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_config["EmailSettings:SmtpPort"]),
                    Credentials = new NetworkCredential(_config["EmailSettings:SmtpUsername"], _config["EmailSettings:SmtpPassword"]),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_config["EmailSettings:SmtpUsername"], "Shop Online"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

