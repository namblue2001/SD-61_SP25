using System;
using System.Collections.Generic;
using System.Linq;
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
        public AccessController( ApplicationDbContext context)
        {
           
            _context = context;
        }

        public IActionResult DangKy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DangKy(TaiKhoan user )

        {
            var existingUser = await _context.TaiKhoan.FirstOrDefaultAsync(u => u.taiKhoan == user.taiKhoan);
            if (existingUser != null)
            {
                TempData["Error"] = "Tên đăng nhập đã tồn tại";
                return View();
            }
            var existingEmail = await _context.TaiKhoan.FirstOrDefaultAsync(u => u.email == user.email);
            if (existingEmail != null)
            {
                TempData["Error1"] = "Email đã tồn tại";
                return View();
            }
            var taikhoan = new TaiKhoan()
            {
                hoTen = user.hoTen,
                email = user.email,
                soDienThoai = user.soDienThoai,
                ngaySinh = user.ngaySinh,
                gioiTinh = user.gioiTinh,
                taiKhoan = user.taiKhoan,
                matKhau = user.matKhau
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
                HttpContext.Session.SetString("id_taikhoan" , taikhoandangnhap.ID_TaiKhoan.ToString());
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
                else if(taikhoandangnhap.trangThai == "Ngừng hoạt động")
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
    }
}

