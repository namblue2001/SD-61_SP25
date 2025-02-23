using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleTee.Data;
using StyleTee.Models;
using StyleTee.Models.ViewModel;
using StyleTee.Service;
using StyleTee.Service.IService;


namespace StyleTee.Controllers
{
    public class AccessController : Controller
    {
        private readonly ITaiKhoanService _tkservice;
        private readonly ApplicationDbContext _context;
        public AccessController(ITaiKhoanService tkservice , ApplicationDbContext context)
        {
            _tkservice = tkservice;
            _context = context;
        }

        public IActionResult DangKy()
        {
            return View();
        }


        [HttpPost]
        public IActionResult DangKy(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vaitro = _context.VaiTro.FirstOrDefault(v => v.tenVaiTro == "KhachHang");
                if(vaitro != null)
                {
                    vaitro = new VaiTro { ID_VaiTro = Guid.NewGuid(), tenVaiTro = "KhachHang" };
                    _context.VaiTro.Add(vaitro);
                    _context.SaveChanges();
                }
      
                var user = new TaiKhoan()
                {
                    ID_TaiKhoan = Guid.NewGuid(),
                    taiKhoan = model.tenDangNhap,
                    matKhau = model.matKhau,
                    ID_VaiTro = vaitro.ID_VaiTro
                };
                var registerUer = _tkservice.Register(user);
                return RedirectToAction("DangNhap");
            }
            else
            {
                return View(model);
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
    }
}

