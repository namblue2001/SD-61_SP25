using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using StyleTee.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace StyleTee.Areas.QuanLy.Controllers
{
    [Area("QuanLy")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var sanPhams = await _context.SanPham
                .Include(s => s.HinhAnh)
                .ToListAsync();
            return View(sanPhams);
        }

        // ... existing controller code ...
    }
} 