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
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Chuyển hướng đến trang danh sách sản phẩm
                }
                catch (Exception)
                {
                    // Xử lý lỗi nếu cần
                    throw;
                }
        }
    }
}
