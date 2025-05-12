using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;

namespace StyleTee.Areas.QuanLy.Controllers
{
    [Area("QuanLy")]
    [Route("QuanLy")]
    [Route("QuanLy/KhachHang")]
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
            var khachHangs = _context.TaiKhoan.Where(x => x.tenVaiTro == "Khách Hàng");
            return View(await khachHangs.ToListAsync());
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string hoTen, string email, string soDienThoai, DateTime ngaySinh, string gioiTinh, string taiKhoan, string matKhau, IFormFile? imageFile)
        {
            var khachHang = new TaiKhoan
            {
                hoTen = hoTen,
                email = email,
                soDienThoai = soDienThoai,
                ngaySinh = ngaySinh,
                gioiTinh = gioiTinh,
                taiKhoan = taiKhoan,
                matKhau = matKhau,
                ID_TaiKhoan = Guid.NewGuid(),
                trangThai = "Hoạt động",
                tenVaiTro = "Khách Hàng"
            };

            if (imageFile != null && imageFile.FileName != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", imageFile.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                imageFile.CopyTo(stream);
                khachHang.anhDaiDien = imageFile.FileName;
            }

            try
            {
                _context.TaiKhoan.Add(khachHang);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["loi"] = "Thông tin không hợp lệ. Vui lòng kiểm tra lại.";
                return View(khachHang);
            }
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var khachHang = _context.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == id);
            return View(khachHang);
        }
        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaiKhoan khachHang, IFormFile? imageFile, Guid id)
        {
            var existing = _context.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == id);
            if (existing != null)
            {
                existing.hoTen = khachHang.hoTen;
                existing.email = khachHang.email;
                existing.soDienThoai = khachHang.soDienThoai;
                existing.ngaySinh = khachHang.ngaySinh;
                existing.trangThai = khachHang.trangThai;
                existing.taiKhoan = khachHang.taiKhoan;

                if (imageFile != null && !string.IsNullOrEmpty(imageFile.FileName))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", imageFile.FileName);
                    using var stream = new FileStream(path, FileMode.Create);
                    imageFile.CopyTo(stream);
                    existing.anhDaiDien = imageFile.FileName;
                }

                _context.TaiKhoan.Update(existing);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            var khachHang = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.ID_TaiKhoan == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }
    }
}
