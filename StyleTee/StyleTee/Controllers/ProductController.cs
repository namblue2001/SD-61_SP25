using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using StyleTee.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace StyleTee.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sanPhams = await _context.SanPham.ToListAsync();
            return View(sanPhams);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            
                try
                {
                    
                    var product = new SanPham
                    {
                        ID_SanPham = Guid.NewGuid(),
                        tenSanPham = sanPham.tenSanPham,
                        moTa = sanPham.moTa,
                        giaGoc = sanPham.giaGoc,
                        trangThai = sanPham.trangThai
                    };
                    _context.SanPham.Add(product);
                    _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Chuyển hướng đến trang danh sách sản phẩm
                }
                catch (Exception)
                {
                    // Xử lý lỗi nếu cần
                    throw;
                }
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SanPham sanPham)
        {
            if (id != sanPham.ID_SanPham)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách sản phẩm
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.ID_SanPham))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sản phẩm. Vui lòng thử lại.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
                }
            return View(sanPham);
        }

        private bool SanPhamExists(Guid id)
        {
            return _context.SanPham.Any(e => e.ID_SanPham == id);
        }
    }
}
