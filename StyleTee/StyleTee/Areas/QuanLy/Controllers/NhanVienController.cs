using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StyleTee.Areas.QuanLy.Controllers
{
    [Area("QuanLy")]
    [Route("QuanLy")]
    [Route("QuanLy/NhanVien")]
    public class NhanVienController : Controller
    {
        private readonly ApplicationDbContext _context;


        public NhanVienController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Xóa cache của context để đảm bảo lấy dữ liệu mới nhất
                _context.ChangeTracker.Clear();

                var nhanvien = await _context.TaiKhoan
                    .Where(x => x.tenVaiTro == "Nhân Viên")
                    .AsNoTracking() // Không theo dõi thay đổi để tránh cache
                    .Select(x => new TaiKhoan
                    {
                        ID_TaiKhoan = x.ID_TaiKhoan,
                        hoTen = x.hoTen,
                        taiKhoan = x.taiKhoan,
                        email = x.email,
                        soDienThoai = x.soDienThoai,
                        gioiTinh = x.gioiTinh,
                        ngaySinh = x.ngaySinh,
                        trangThai = x.trangThai,
                        anhDaiDien = x.anhDaiDien
                    })
                    .ToListAsync();

                // Kiểm tra và cập nhật đường dẫn ảnh
                foreach (var nv in nhanvien)
                {
                    if (!string.IsNullOrEmpty(nv.anhDaiDien))
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", nv.anhDaiDien);
                        if (!System.IO.File.Exists(imagePath))
                        {
                            nv.anhDaiDien = null; // Reset ảnh nếu file không tồn tại
                        }
                    }
                }

                return View(nhanvien);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách nhân viên: " + ex.Message;
                return View(new List<TaiKhoan>());
            }
        }

        [Route("Create")]

        public IActionResult Create()
        {
            return View();
        }
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaiKhoan nhanvien, IFormFile? imageFile)
        {
            try
            {
                var existingAccount = _context.TaiKhoan.FirstOrDefault(x => x.taiKhoan == nhanvien.taiKhoan);
                if (existingAccount != null)
                {
                    ModelState.AddModelError("taiKhoan", "Tài khoản này đã tồn tại!");
                    return View(nhanvien);
                }

                // Kiểm tra email đã tồn tại chưa
                var existingEmail = _context.TaiKhoan.FirstOrDefault(x => x.email == nhanvien.email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("email", "Email này đã được sử dụng!");
                    return View(nhanvien);
                }
                if (string.IsNullOrEmpty(nhanvien.taiKhoan) || string.IsNullOrEmpty(nhanvien.matKhau) ||
                   string.IsNullOrEmpty(nhanvien.email) || string.IsNullOrEmpty(nhanvien.hoTen) ||
                   string.IsNullOrEmpty(nhanvien.soDienThoai))
                {
                    ModelState.AddModelError("", "Vui lòng điền đầy đủ thông tin bắt buộc!");
                    return View(nhanvien);
                }

                // Xử lý upload ảnh
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Kiểm tra định dạng file
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("imageFile", "Chỉ chấp nhận file ảnh có định dạng: .jpg, .jpeg, .png, .gif");
                        return View(nhanvien);
                    }

                    // Tạo tên file duy nhất
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);

                    // Tạo thư mục nếu chưa tồn tại
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Lưu file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // Lưu tên file vào database
                    nhanvien.anhDaiDien = fileName;
                }
                nhanvien.ID_TaiKhoan = Guid.NewGuid();
                nhanvien.trangThai = "Hoạt động";
                nhanvien.tenVaiTro = "Nhân Viên";
                _context.TaiKhoan.Add(nhanvien);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể thêm nhân viên. Vui lòng thử lại!");
                    return View(nhanvien);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm nhân viên: " + ex.Message);
                return View(nhanvien);

            }
        }
        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var staff = _context.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }
        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaiKhoan nhanvien, IFormFile? imageFile)
        {
            try
            {
                // Tìm nhân viên cần sửa
                var staff = await _context.TaiKhoan.FindAsync(nhanvien.ID_TaiKhoan);
                if (staff == null)
                {
                    return NotFound();
                }

                // Kiểm tra tài khoản trùng
                var checkAccount = await _context.TaiKhoan
                    .FirstOrDefaultAsync(x => x.taiKhoan == nhanvien.taiKhoan && x.ID_TaiKhoan != nhanvien.ID_TaiKhoan);
                if (checkAccount != null)
                {
                    ModelState.AddModelError("taiKhoan", "Tài khoản này đã tồn tại!");
                    return View(nhanvien);
                }

                // Kiểm tra email trùng
                var checkEmail = await _context.TaiKhoan
                    .FirstOrDefaultAsync(x => x.email == nhanvien.email && x.ID_TaiKhoan != nhanvien.ID_TaiKhoan);
                if (checkEmail != null)
                {
                    ModelState.AddModelError("email", "Email này đã được sử dụng!");
                    return View(nhanvien);
                }

                // Xử lý ảnh
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Xóa ảnh cũ
                    if (!string.IsNullOrEmpty(staff.anhDaiDien))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", staff.anhDaiDien);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    // Lưu ảnh mới
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    staff.anhDaiDien = fileName;
                }

                // Cập nhật thông tin
                staff.hoTen = nhanvien.hoTen;
                staff.taiKhoan = nhanvien.taiKhoan;
                staff.email = nhanvien.email;
                staff.soDienThoai = nhanvien.soDienThoai;
                staff.gioiTinh = nhanvien.gioiTinh;
                staff.ngaySinh = nhanvien.ngaySinh;
                staff.trangThai = nhanvien.trangThai;

                // Cập nhật mật khẩu nếu có
                if (!string.IsNullOrEmpty(nhanvien.matKhau))
                {
                    staff.matKhau = nhanvien.matKhau;
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                return View(nhanvien);
            }
        }
        [Route("Details")]


        public async Task<IActionResult> Details(Guid? id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return NotFound();
                }

                var employee = await _context.TaiKhoan
                    .FirstOrDefaultAsync(x => x.ID_TaiKhoan == id);

                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }
            catch (Exception ex)
            {
                // Log the error here if you have logging configured
                return RedirectToAction("Index");
            }
        }
    }
}

