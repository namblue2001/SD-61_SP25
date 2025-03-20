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
    [Route("NhanVien/SanPham")]
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SanPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            Trang();
            var applicationDbContext = _context.SanPham.Include(s => s.DanhMuc).OrderBy(a => a.trangThai == "Hoạt động" ? 0 : 1).OrderByDescending(n => n.ngayTao);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("Index")]
        [HttpPost]
        public async Task<IActionResult> Index(string? ten)
        {
            Trang();
            if (ten != null)
            {
                return _context.SanPham.Include(s => s.DanhMuc).Where(n => n.tenSanPham.ToLower().Contains(ten.ToLower())).OrderBy(a => a.trangThai == "Hoạt động" ? 0 : 1).OrderByDescending(n => n.ngayTao) != null ?
                              View(await _context.SanPham.Include(s => s.DanhMuc).Where(n => n.tenSanPham.ToLower().Contains(ten.ToLower())).OrderBy(a => a.trangThai == "Hoạt động" ? 0 : 1).OrderByDescending(n => n.ngayTao).ToListAsync()) :
                              Problem("Entity set 'ApplicationDbContext.SanPham'  is null.");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("SanPhamChiTiet")]
        public IActionResult SanPhamChiTiet(Guid id)
        {
            Trang();
            var sanphamchitiet = _context.SanPhamChiTiet.Include(a => a.SanPham).ThenInclude(a => a.DanhMuc).Include(a => a.MauSac).Include(a => a.XuatXu).Include(a => a.ThuongHieu).Include(a => a.KichThuoc).Include(a => a.ChatLieu).Include(a => a.KieuDang).Where(a => a.ID_SanPham == id).OrderBy(a => a.MauSac.tenMauSac).DefaultIfEmpty().ToList();
            var sanpham = _context.SanPham.FirstOrDefault(a => a.ID_SanPham == id);
            TempData["sanpham"] = sanpham.tenSanPham;
            return View(sanphamchitiet);
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            Trang();
            var sanphamchitiet = _context.SanPhamChiTiet.Include(a => a.SanPham).ThenInclude(a => a.DanhMuc).Include(a => a.MauSac).Include(a => a.XuatXu).Include(a => a.ThuongHieu).Include(a => a.KichThuoc).Include(a => a.ChatLieu).Include(a => a.KieuDang).FirstOrDefault(a => a.ID_SanPhamChiTiet == id);
            return View(sanphamchitiet);
        }

        public void Trang()
        {
            TempData["Trang"] = "Theo dõi sản phẩm";
        }
    }
}
