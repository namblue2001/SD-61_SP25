using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;

namespace StyleTee.Areas.QuanLy.Controllers.SanPhamVaThuocTinh
{
    [Area("QuanLy")]
    [Route("QuanLy")]
    [Route("QuanLy/SanPham")]
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
            var applicationDbContext = _context.SanPham;
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("Index")]
        [HttpPost]
        public async Task<IActionResult> Index(string ten)
        {
            Trang();
            return _context.SanPham.Where(n => n.tenSanPham.ToLower().Contains(ten.ToLower())).OrderBy(a => a.trangThai == "Hoạt động" ? 0 : 1).ThenBy(a => a.tenSanPham) != null ?
                          View(await _context.SanPham.Where(n => n.tenSanPham.ToLower().Contains(ten.ToLower())).OrderBy(a => a.trangThai == "Hoạt động" ? 0 : 1).ThenBy(a => a.tenSanPham).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SanPham'  is null.");
        }

        [Route("Create")]
        public IActionResult Create()
        {
            Trang();
            ViewData["ID_DanhMuc"] = new SelectList(_context.DanhMuc, "ID_DanhMuc", "tenDanhMuc");
            SanPham sanpham = new SanPham();
            return View(sanpham);
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham, IFormFile formFile)
        {
            Trang();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LayoutAdmin/img/SanPham", formFile.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            sanPham.trangThai = "Hoạt động";
            sanPham.anhDaiDien = formFile.FileName;
            try
            {
                sanPham.ID_SanPham = Guid.NewGuid();
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["ID_DanhMuc"] = new SelectList(_context.DanhMuc, "ID_DanhMuc", "tenDanhMuc", sanPham.ID_DanhMuc);
                return View(sanPham);
            }
        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Trang();
            var sanPham = _context.SanPham.FirstOrDefault(a => a.ID_SanPham == id);
            ViewData["ID_DanhMuc"] = new SelectList(_context.DanhMuc, "ID_DanhMuc", "tenDanhMuc", sanPham.ID_DanhMuc);
            return View(sanPham);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SanPham sanPham, IFormFile? formFile)
        {
            Trang();
            if (formFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LayoutAdmin/img/SanPham", formFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                sanPham.anhDaiDien = formFile.FileName;
            }
            try
            {
                _context.Update(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["ID_DanhMuc"] = new SelectList(_context.DanhMuc, "ID_DanhMuc", "tenDanhMuc", sanPham.ID_DanhMuc);
                return View(sanPham);
            }

        }

        public void Trang()
        {
        
        }
    }

}
