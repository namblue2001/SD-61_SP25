using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using StyleTee.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Globalization;

namespace StyleTee.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var sanPhams = from s in _context.SanPham
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s => s.tenSanPham.Contains(searchString));
            }

            return View(await sanPhams.ToListAsync());
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
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.ID_SanPham))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        private bool SanPhamExists(Guid id)
        {
            return _context.SanPham.Any(e => e.ID_SanPham == id);
        }
    }
}