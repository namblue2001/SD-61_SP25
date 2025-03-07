using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly ApplicationDbContext _context;


        public NhanVienController( ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var nhanvien = _context.TaiKhoan.Where(x => x.tenVaiTro == "Nhân Viên").DefaultIfEmpty();
            return View(nhanvien);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(string hoTen, string email, string soDienThoai, DateTime ngaySinh, string gioiTinh, string taiKhoan, string matKhau, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var nhanvien = new TaiKhoan
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
                    tenVaiTro = "Nhân Viên",
                };
                if (imageFile != null && imageFile.FileName != null)
                {

                    // Thực hiện trỏ tới thu mục Root để copy file từ ngoài vào
                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot", "Image", imageFile.FileName);
                    // Kết quả thu được sẽ có dạng ~wwwroot/img/filename
                    var stream = new FileStream(path, FileMode.Create); // Mode = Create vì ta copy
                    imageFile.CopyTo(stream); // Copy ảnh vào stream có path là path mình vừa truyền
                                              // Gán lại thuộc tính imageURL = đường dẫn vào file trong root

                    nhanvien.anhDaiDien = imageFile.FileName;
                };
                try
                {
                    _context.TaiKhoan.Add(nhanvien);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "NhanVien");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["lỗi"] = "Thông tin bạn nhập không đúng. Vui lòng kiểm tra lại";
                    return View(nhanvien);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var staff = _context.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == id);
            return View(staff);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaiKhoan nhanvien, IFormFile? imageFile, Guid id)
        {

            // Thực hiện trỏ tới thu mục Root để copy file từ ngoài vào
            var staff = _context.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == id);
            if (staff != null)
            {
                staff.hoTen = nhanvien.hoTen;
                staff.email = nhanvien.email;
                staff.soDienThoai = nhanvien.soDienThoai;

                if (imageFile != null && !string.IsNullOrEmpty(imageFile.FileName))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                   "wwwroot", "Image", imageFile.FileName);
                    // Kết quả thu được sẽ có dạng ~wwwroot/img/filename
                    var stream = new FileStream(path, FileMode.Create); // Mode = Create vì ta copy
                    imageFile.CopyTo(stream); // Copy ảnh vào stream có path là path mình vừa truyền
                                              // Gán lại thuộc tính imageURL = đường dẫn vào file trong root
                    staff.anhDaiDien = imageFile.FileName;
                }
                staff.ngaySinh = nhanvien.ngaySinh;
                staff.trangThai = nhanvien.trangThai;
                staff.taiKhoan = nhanvien.taiKhoan;
                staff.matKhau = nhanvien.matKhau;
                _context.TaiKhoan.Update(staff);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanvien);

        }

        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.ID_TaiKhoan == id);
            if (employee == null)
            {
                return NotFound();
            }
            var viewModel = new TaiKhoan
            {
                ID_TaiKhoan = employee.ID_TaiKhoan,
                taiKhoan = employee.taiKhoan,
                matKhau = employee.matKhau,
                email = employee.email,
                hoTen = employee.hoTen,
                soDienThoai = employee.soDienThoai,
                anhDaiDien = employee.anhDaiDien,
                gioiTinh = employee.gioiTinh,
                ngaySinh = employee.ngaySinh,
                trangThai = employee.trangThai,
                tenVaiTro = employee.tenVaiTro
            };
            return View(viewModel);
        }
    }
}
