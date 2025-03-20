using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models.PhieuGiamGiaVaKhuyenMai;

namespace StyleTee.Areas.QuanLy.Controllers
{
    [Area("QuanLy")]
    [Route("QuanLy")]
    [Route("QuanLy/PhieuGiamGia")]
    public class PhieuGiamGiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhieuGiamGiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            Trang();
            ViewBag.KhuyenMai = _context.KhuyenMai.ToList();
            return _context.PhieuGiamGia != null ? View(await _context.PhieuGiamGia.ToListAsync()) : Problem("Entity set 'ApplicationDbContext.PhieuGiamGia'  is null.");
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            Trang();
            var phieuGiamGia = await _context.PhieuGiamGia.FirstOrDefaultAsync(m => m.Ma_PhieuGiamGia == id);
            return View(phieuGiamGia);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            Trang();
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuGiamGia phieuGiamGia)
        {
            Trang();
            if (await _context.PhieuGiamGia.AnyAsync(t => t.TenPhieuGiamGia == phieuGiamGia.TenPhieuGiamGia && t.NgayHetHan >= DateTime.Now))
            {
                ModelState.AddModelError("NgayHetHan", "Phiếu giảm giá đã tồn tại và còn hạn sử dụng.");
                return View(phieuGiamGia);
            }
            try
            {
                phieuGiamGia.Ma_PhieuGiamGia = Guid.NewGuid();
                _context.Add(phieuGiamGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(phieuGiamGia);
            }
        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Trang();
            var phieuGiamGia = await _context.PhieuGiamGia.FindAsync(id);
            return View(phieuGiamGia);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PhieuGiamGia phieuGiamGia)
        {
            Trang();
            try
            {
                _context.Update(phieuGiamGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(phieuGiamGia);
            }            
        }

        public void Trang()
        {
            TempData["Trang"] = "Quản lý khuyến mãi";
        }

        private bool PhieuGiamGiaExists(Guid id)
        {
            return (_context.PhieuGiamGia?.Any(e => e.Ma_PhieuGiamGia == id)).GetValueOrDefault();
        }
    }
}
