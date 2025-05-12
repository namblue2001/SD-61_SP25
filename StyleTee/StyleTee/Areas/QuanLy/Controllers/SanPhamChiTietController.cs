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
            // Get existing products with their details
            var existingProducts = _context.SanPhamChiTiet
                .Include(x => x.SanPham)
                .Include(x => x.ChatLieu)
                .Include(x => x.KichThuoc)
                .Include(x => x.MauSac)
                .Include(x => x.ThuongHieu)
                .Include(x => x.KieuDang)
                .Include(x => x.XuatXu)
                .OrderByDescending(x => x.ngayTao)
                .Take(10) // Show only the 10 most recent products
                .ToList();

            ViewBag.ExistingProducts = existingProducts;
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
        public async Task<IActionResult> Create([Bind("ID_SanPhamChiTiet,ID_SanPham,ID_Mau,ID_XuatXu,ID_ThuongHieu,ID_Size,ID_KieuDang,ID_ChatLieu,giaBan,anhDaiDien,ngayTao,soLuongTon")] SanPhamChiTiet sanPhamChiTiet, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile != null && formFile.Length > 0)
                {
                    var fileName = Path.GetFileName(formFile.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LayoutAdmin", "img", "SanPhamChiTiet", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    sanPhamChiTiet.anhDaiDien = uniqueFileName;
                }

                sanPhamChiTiet.ID_SanPhamChiTiet = Guid.NewGuid();
                sanPhamChiTiet.ngayTao = DateTime.Now;
                _context.Add(sanPhamChiTiet);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm sản phẩm chi tiết thành công!";
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, lấy lại dữ liệu cho các dropdown
            ViewBag.ID_SanPham = new SelectList(_context.SanPham, "ID_SanPham", "tenSanPham", sanPhamChiTiet.ID_SanPham);
            ViewBag.ID_MauSac = new SelectList(_context.MauSac, "ID_Mau", "tenMauSac", sanPhamChiTiet.ID_Mau);
            ViewBag.ID_XuatXu = new SelectList(_context.XuatXu, "ID_XuatXu", "tenXuatXu", sanPhamChiTiet.ID_XuatXu);
            ViewBag.ID_ThuongHieu = new SelectList(_context.ThuongHieu, "ID_ThuongHieu", "tenThuongHieu", sanPhamChiTiet.ID_ThuongHieu);
            ViewBag.ID_KichThuoc = new SelectList(_context.KichThuoc, "ID_Size", "tenKichThuoc", sanPhamChiTiet.ID_Size);
            ViewBag.ID_KieuDang = new SelectList(_context.KieuDang, "ID_KieuDang", "tenKieuDang", sanPhamChiTiet.ID_KieuDang);
            ViewBag.ID_ChatLieu = new SelectList(_context.ChatLieu, "ID_ChatLieu", "tenChatLieu", sanPhamChiTiet.ID_ChatLieu);

            // Lấy danh sách sản phẩm hiện có
            ViewBag.ExistingProducts = await _context.SanPhamChiTiet
                .Include(sp => sp.SanPham)
                .Include(sp => sp.ChatLieu)
                .Include(sp => sp.KichThuoc)
                .Include(sp => sp.MauSac)
                .Include(sp => sp.ThuongHieu)
                .Include(sp => sp.KieuDang)
                .Include(sp => sp.XuatXu)
                .OrderByDescending(sp => sp.ngayTao)
                .Take(10)
                .ToListAsync();

            return View(sanPhamChiTiet);
        }

        public void Trang()
        {

        }

        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spct = await _context.SanPhamChiTiet
                .Include(x => x.SanPham)
                .Include(x => x.ChatLieu)
                .Include(x => x.KichThuoc)
                .Include(x => x.MauSac)
                .Include(x => x.ThuongHieu)
                .Include(x => x.KieuDang)
                .Include(x => x.XuatXu)
                .FirstOrDefaultAsync(x => x.ID_SanPhamChiTiet == id);

            if (spct == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Edit(SanPhamChiTiet spct, IFormFile? formFile)
        {
            try
            {
                // Tìm sản phẩm chi tiết cần sửa
                var existingProduct = await _context.SanPhamChiTiet.FindAsync(spct.ID_SanPhamChiTiet);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Xử lý ảnh
                if (formFile != null && formFile.Length > 0)
                {
                    // Xóa ảnh cũ
                    if (!string.IsNullOrEmpty(existingProduct.anhDaiDien))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LayoutAdmin/img/SanPhamChiTiet", existingProduct.anhDaiDien);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    // Lưu ảnh mới
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LayoutAdmin/img/SanPhamChiTiet", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    existingProduct.anhDaiDien = fileName;
                }

                // Cập nhật thông tin
                existingProduct.ID_SanPham = spct.ID_SanPham;
                existingProduct.ID_Mau = spct.ID_Mau;
                existingProduct.ID_XuatXu = spct.ID_XuatXu;
                existingProduct.ID_ThuongHieu = spct.ID_ThuongHieu;
                existingProduct.ID_Size = spct.ID_Size;
                existingProduct.ID_KieuDang = spct.ID_KieuDang;
                existingProduct.ID_ChatLieu = spct.ID_ChatLieu;
                existingProduct.giaBan = spct.giaBan;
                existingProduct.soLuongTon = spct.soLuongTon;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                // Load lại các dropdown list
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
