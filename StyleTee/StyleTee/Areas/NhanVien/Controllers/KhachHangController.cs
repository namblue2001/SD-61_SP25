using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;

namespace StyleTee.Areas.NhanVien.Controllers
{
    [Area("NhanVien")]
    [Route("NhanVien")]
    [Route("NhanVien/KhachHang")]
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            Trang();
            return _context.TaiKhoan.Where(a => a.tenVaiTro == "Khách hàng") != null ?
                        View(await _context.TaiKhoan.Where(a => a.tenVaiTro == "Khách hàng").ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.TaiKhoanDangNhap'  is null.");
        }

        [Route("Index")]
        [HttpPost]
        public async Task<IActionResult> Index(string? ten)
        {
            Trang();
            if (ten != null)
            {
                return _context.TaiKhoan.Where(n => n.hoTen.ToLower().Contains(ten.ToLower()) && n.tenVaiTro == "Khách hàng").OrderBy(a => a.trangThai == "Hoạt động" ? 0 : 1).ThenBy(a => a.hoTen) != null ?
                              View(await _context.TaiKhoan.Where(n => n.hoTen.ToLower().Contains(ten.ToLower()) && n.tenVaiTro == "Khách hàng").OrderBy(a => a.trangThai == "Hoạt động" ? 0 : 1).ThenBy(a => a.hoTen).ToListAsync()) :
                              Problem("Entity set 'ApplicationDbContext.NhaCungCap'  is null.");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            Trang();
            var taiKhoan = await _context.TaiKhoan.Include(a => a.DiaChis).FirstOrDefaultAsync(m => m.ID_TaiKhoan == id);

            return View(taiKhoan);
        }

        public void Trang()
        {
            TempData["Trang"] = "Theo dõi khách hàng";
        }

    }
}

