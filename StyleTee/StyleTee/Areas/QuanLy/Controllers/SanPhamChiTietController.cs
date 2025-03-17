using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;

namespace StyleTee.Areas.QuanLy.Controllers
{
    [Area("QuanLy")]
    [Route("QuanLy")]
    [Route("QuanLy/SanPhamChiTiet")]
    public class SanPhamChiTietController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SanPhamChiTietController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Index")]

        public async Task<IActionResult> Index()
        {
            var product = _context.SanPhamChiTiet
                .Include(x => x.XuatXu)
                .Include(x => x.ChatLieu)
                .Include(x => x.KichThuoc)
                .Include(x => x.MauSac)
                .Include(x => x.ThuongHieu)
                .Include(x => x.KieuDang)
                .Include(x => x.SanPham);
            return View(await product.ToListAsync());
        }
        [Route("Details")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SanPhamChiTiet == null)
            {
                return NotFound();
            }

            var spct = await _context.SanPhamChiTiet
                .Include(x => x.XuatXu)
                .Include(x => x.ChatLieu)
                .Include(x => x.KichThuoc)
                .Include(x => x.MauSac)
                .Include(x => x.ThuongHieu)
                .Include(x => x.KieuDang)
                .Include(x => x.SanPham)
                .FirstOrDefaultAsync(m => m.ID_SanPhamChiTiet == id);
            if (spct == null)
            {
                return NotFound();
            }

            return View(spct);
        }


        [Route("Create")]
        public IActionResult Create()
        {
            ViewBag.ID_XuatXu = new SelectList(_context.XuatXu, "ID_XuatXu", "tenXuatXu");
            ViewBag.ID_ChatLieu = new SelectList(_context.ChatLieu, "ID_ChatLieu", "tenChatLieu");
            ViewBag.ID_KichThuoc = new SelectList(_context.KichThuoc, "ID_KichThuoc", "tenKichThuoc");
            ViewBag.ID_MauSac = new SelectList(_context.MauSac, "ID_MauSac", "tenMauSac");
            ViewBag.ID_ThuongHieu = new SelectList(_context.ThuongHieu, "ID_ThuongHieu", "tenThuongHieu");
            ViewBag.ID_KieuDang = new SelectList(_context.KieuDang, "ID_KieuDang", "tenKieuDang");
            ViewBag.ID_SanPham = new SelectList(_context.SanPham, "ID_SanPham", "tenSanPham");
            SanPhamChiTiet spct = new SanPhamChiTiet();
            return View(spct);

        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPhamChiTiet spct, IFormFile formFile)
        {

            Trang();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LayoutAdmin/img/SanPhamChiTiet", formFile.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            spct.anhDaiDien = formFile.FileName;

            try
            {
                spct.ID_SanPhamChiTiet = Guid.NewGuid();
                _context.Add(spct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewBag.ID_XuatXu = new SelectList(_context.XuatXu, "ID_XuatXu", "tenXuatXu", spct.ID_XuatXu);
                ViewBag.ID_ChatLieu = new SelectList(_context.ChatLieu, "ID_ChatLieu", "tenChatLieu", spct.ID_ChatLieu);
                ViewBag.ID_KichThuoc = new SelectList(_context.KichThuoc, "ID_KichThuoc", "tenKichThuoc", spct.ID_Size);
                ViewBag.ID_MauSac = new SelectList(_context.MauSac, "ID_MauSac", "tenMauSac", spct.ID_Mau);
                ViewBag.ID_ThuongHieu = new SelectList(_context.ThuongHieu, "ID_ThuongHieu", "tenThuongHieu", spct.ID_ThuongHieu);
                ViewBag.ID_KieuDang = new SelectList(_context.KieuDang, "ID_KieuDang", "tenKieuDang", spct.ID_KieuDang);
                ViewBag.ID_SanPham = new SelectList(_context.SanPham, "ID_SanPham", "tenSanPham", spct.ID_SanPham);
                return View(spct);
            }

        }

        public void Trang()
        {

        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            Trang();
            var spct = _context.SanPhamChiTiet.Include(a => a.SanPham).FirstOrDefault(a => a.ID_SanPhamChiTiet == id);
            ViewBag.ID_XuatXu = new SelectList(_context.XuatXu, "ID_XuatXu", "tenXuatXu", spct.ID_XuatXu);
            ViewBag.ID_ChatLieu = new SelectList(_context.ChatLieu, "ID_ChatLieu", "tenChatLieu", spct.ID_ChatLieu);
            ViewBag.ID_KichThuoc = new SelectList(_context.KichThuoc, "ID_KichThuoc", "tenKichThuoc", spct.ID_Size);
            ViewBag.ID_MauSac = new SelectList(_context.MauSac, "ID_MauSac", "tenMauSac", spct.ID_Mau);
            ViewBag.ID_ThuongHieu = new SelectList(_context.ThuongHieu, "ID_ThuongHieu", "tenThuongHieu", spct.ID_ThuongHieu);
            ViewBag.ID_KieuDang = new SelectList(_context.KieuDang, "ID_KieuDang", "tenKieuDang", spct.ID_KieuDang);
            ViewBag.ID_SanPham = new SelectList(_context.SanPham, "ID_SanPham", "tenSanPham", spct.ID_SanPham);
            return View(spct);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( SanPhamChiTiet spct , IFormFile? formFile)
        {
            //try
            //{
            //    _context.Update(spct);
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!SanPhamExists(spct.ID_SanPhamChiTiet))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            //return RedirectToAction(nameof(Index));
            Trang();
            if (formFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LayoutAdmin/img/SanPhamChiTiet", formFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                spct.anhDaiDien = formFile.FileName;
            }
            try
            {
                _context.Update(spct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewBag.ID_XuatXu = new SelectList(_context.XuatXu, "ID_XuatXu", "tenXuatXu", spct.ID_XuatXu);
                ViewBag.ID_ChatLieu = new SelectList(_context.ChatLieu, "ID_ChatLieu", "tenChatLieu", spct.ID_ChatLieu);
                ViewBag.ID_KichThuoc = new SelectList(_context.KichThuoc, "ID_KichThuoc", "tenKichThuoc", spct.ID_Size);
                ViewBag.ID_MauSac = new SelectList(_context.MauSac, "ID_MauSac", "tenMauSac", spct.ID_Mau);
                ViewBag.ID_ThuongHieu = new SelectList(_context.ThuongHieu, "ID_ThuongHieu", "tenThuongHieu", spct.ID_ThuongHieu);
                ViewBag.ID_KieuDang = new SelectList(_context.KieuDang, "ID_KieuDang", "tenKieuDang", spct.ID_KieuDang);
                ViewBag.ID_SanPham = new SelectList(_context.SanPham, "ID_SanPham", "tenSanPham", spct.ID_SanPham);
                return View(spct);
            }


        }

        private bool SanPhamExists(Guid id)
        {
            return (_context.SanPhamChiTiet?.Any(e => e.ID_SanPhamChiTiet == id)).GetValueOrDefault();
        }
    }
}
