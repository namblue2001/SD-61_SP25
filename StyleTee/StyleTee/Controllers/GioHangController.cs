using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StyleTee.Models;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;

namespace StyleTee.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "GioHang";

        public GioHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị giỏ hàng
        public async Task<IActionResult> Index()
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }

            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.SanPham)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.HinhAnh)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.KichThuoc)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.MauSac)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (gioHang == null)
            {
                // Nếu chưa có giỏ hàng, tạo mới
                gioHang = new GioHang
                {
                    ID_GioHang = Guid.NewGuid(),
                    ID_TaiKhoan = Guid.Parse(idTaiKhoan),
                    ngayTao = DateTime.Now,
                    GioHangChiTiet = new List<GioHangChiTiet>()
                };
                _context.GioHang.Add(gioHang);
                await _context.SaveChangesAsync();
            }

            return View(gioHang);
        }

        // Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public IActionResult AddToCart(Guid idSanPhamChiTiet, string tenSanPham, string anhDaiDien, decimal donGia, int soLuong)
        {
            var gioHang = GetGioHangFromSession();

            var chiTiet = gioHang.ChiTietGioHang.FirstOrDefault(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);
            if (chiTiet != null)
            {
                chiTiet.SoLuong += soLuong;
            }
            else
            {
                gioHang.ChiTietGioHang.Add(new GioHangChiTietViewModel
                {
                    ID_SanPhamChiTiet = idSanPhamChiTiet,
                    TenSanPham = tenSanPham,
                    AnhDaiDien = anhDaiDien,
                    DonGia = donGia,
                    SoLuong = soLuong
                });
            }

            SaveGioHangToSession(gioHang);
            return RedirectToAction("Index");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public IActionResult RemoveFromCart(Guid idSanPhamChiTiet)
        {
            var gioHang = GetGioHangFromSession();
            gioHang.ChiTietGioHang.RemoveAll(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);
            SaveGioHangToSession(gioHang);

            return RedirectToAction("Index");
        }

        // Lấy giỏ hàng từ Session
        private GioHangViewModel GetGioHangFromSession()
        {
            var gioHangJson = HttpContext.Session.GetString(CartSessionKey);
            return gioHangJson == null ? new GioHangViewModel() : JsonConvert.DeserializeObject<GioHangViewModel>(gioHangJson);
        }

        // Lưu giỏ hàng vào Session
        private void SaveGioHangToSession(GioHangViewModel gioHang)
        {
            HttpContext.Session.SetString(CartSessionKey, JsonConvert.SerializeObject(gioHang));
        }

        // Hiển thị trang xác nhận đơn hàng
        public async Task<IActionResult> XacNhanDonHang()
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }

            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.SanPham)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.HinhAnh)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.KichThuoc)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.MauSac)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (gioHang == null || !gioHang.GioHangChiTiet.Any())
            {
                return RedirectToAction("Index");
            }

            // Lấy thông tin tài khoản và địa chỉ
            var taiKhoan = await _context.TaiKhoan
                .Include(tk => tk.DiaChis)
                .FirstOrDefaultAsync(tk => tk.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (taiKhoan == null)
            {
                return RedirectToAction("DangNhap", "Access");
            }

            // Lấy danh sách địa chỉ của tài khoản
            ViewBag.DanhSachDiaChi = taiKhoan.DiaChis;

            // Lấy địa chỉ mặc định (địa chỉ đầu tiên có trạng thái Hoạt động)
            var diaChiMacDinh = taiKhoan.DiaChis
                .FirstOrDefault(dc => dc.trangThai == "Hoạt động");

            // Tạo danh sách chi tiết đơn hàng với đầy đủ thông tin
            var chiTietDonHang = new List<ChiTietDonHang>();
            foreach (var item in gioHang.GioHangChiTiet)
            {
                var sanPhamChiTiet = await _context.SanPhamChiTiet
                    .Include(sp => sp.SanPham)
                    .Include(sp => sp.HinhAnh)
                    .FirstOrDefaultAsync(sp => sp.ID_SanPhamChiTiet == item.ID_SanPhamChiTiet);

                if (sanPhamChiTiet != null)
                {
                    chiTietDonHang.Add(new ChiTietDonHang
                    {
                        ID_SanPhamChiTiet = item.ID_SanPhamChiTiet,
                        soLuong = item.soLuong,
                        donGia = item.donGia,
                        tongTien = item.donGia * item.soLuong,
                        SanPhamChiTiet = sanPhamChiTiet
                    });
                }
            }

            var donHang = new DonHang
            {
                tongTien = gioHang.GioHangChiTiet.Sum(item => item.donGia * item.soLuong),
                diaChiVanChuyen = diaChiMacDinh != null 
                    ? $"{diaChiMacDinh.soNha}, {diaChiMacDinh.xa}, {diaChiMacDinh.huyen}, {diaChiMacDinh.tinhThanhPho}"
                    : "",
                phuongThucThanhToan = "Thanh toán khi nhận hàng", // Mặc định
                ChiTietDonHang = chiTietDonHang
            };

            return View(donHang);
        }

        // Xử lý đặt hàng
        [HttpPost]
        public async Task<IActionResult> DatHang(DonHang donHang)
        {
            if (!ModelState.IsValid)
            {
                return View("XacNhanDonHang", donHang);
            }

            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }

            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (gioHang == null || !gioHang.GioHangChiTiet.Any())
            {
                return RedirectToAction("Index");
            }

            try
            {
                // Cập nhật thông tin đơn hàng
                donHang.ID_DonHang = Guid.NewGuid();
                donHang.ID_TaiKhoan = Guid.Parse(idTaiKhoan);
                donHang.ngayDatHang = DateTime.Now;
                donHang.trangThaiDonHang = "Chờ xác nhận";
                donHang.trangThaiThanhToan = "Chưa thanh toán";
                donHang.tongTien = gioHang.GioHangChiTiet.Sum(item => item.donGia * item.soLuong);

                _context.DonHang.Add(donHang);

                // Thêm chi tiết đơn hàng
                foreach (var item in gioHang.GioHangChiTiet)
                {
                    var chiTietDonHang = new ChiTietDonHang
                    {
                        ID_DonHangChiTiet = Guid.NewGuid(),
                        ID_DonHang = donHang.ID_DonHang,
                        ID_SanPhamChiTiet = item.ID_SanPhamChiTiet,
                        soLuong = item.soLuong,
                        donGia = item.donGia,
                        tongTien = item.donGia * item.soLuong
                    };

                    _context.ChiTietDonHang.Add(chiTietDonHang);
                }

                // Xóa giỏ hàng
                _context.GioHangChiTiet.RemoveRange(gioHang.GioHangChiTiet);
                _context.GioHang.Remove(gioHang);

                // Lưu vào database
                await _context.SaveChangesAsync();

                return RedirectToAction("DatHangThanhCong");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                ModelState.AddModelError("", "Có lỗi xảy ra khi đặt hàng. Vui lòng thử lại.");
                return View("XacNhanDonHang", donHang);
            }
        }

        // Hiển thị trang đặt hàng thành công
        public IActionResult DatHangThanhCong()
        {
            return View();
        }

        // Hiển thị giỏ hàng từ database
        public async Task<IActionResult> GioHangDB()
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }
            
            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.SanPham)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.HinhAnh)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.KichThuoc)
                .Include(g => g.GioHangChiTiet)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(sp => sp.MauSac)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (gioHang == null)
            {
                // Nếu chưa có giỏ hàng, tạo mới
                gioHang = new GioHang
                {
                    ID_GioHang = Guid.NewGuid(),
                    ID_TaiKhoan = Guid.Parse(idTaiKhoan),
                    ngayTao = DateTime.Now,
                    GioHangChiTiet = new List<GioHangChiTiet>()
                };
                _context.GioHang.Add(gioHang);
                await _context.SaveChangesAsync();
            }

            return View(gioHang);
        }

        // Thêm sản phẩm vào giỏ hàng database
        [HttpPost]
        public async Task<IActionResult> AddToCartDB(Guid idSanPhamChiTiet, int soLuong)
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }
            
            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    ID_GioHang = Guid.NewGuid(),
                    ID_TaiKhoan = Guid.Parse(idTaiKhoan),
                    ngayTao = DateTime.Now,
                    GioHangChiTiet = new List<GioHangChiTiet>()
                };
                _context.GioHang.Add(gioHang);
            }

            var sanPhamChiTiet = await _context.SanPhamChiTiet
                .Include(sp => sp.SanPham)
                .Include(sp => sp.KichThuoc)
                .Include(sp => sp.MauSac)
                .Include(sp => sp.HinhAnh)
                .FirstOrDefaultAsync(sp => sp.ID_SanPhamChiTiet == idSanPhamChiTiet);

            if (sanPhamChiTiet == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }

            var chiTiet = gioHang.GioHangChiTiet
                .FirstOrDefault(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);

            if (chiTiet != null)
            {
                chiTiet.soLuong += soLuong;
            }
            else
            {
                gioHang.GioHangChiTiet.Add(new GioHangChiTiet
                {
                    ID_GioHangChiTiet = Guid.NewGuid(),
                    ID_GioHang = gioHang.ID_GioHang,
                    ID_SanPhamChiTiet = idSanPhamChiTiet,
                    soLuong = soLuong,
                    donGia = sanPhamChiTiet.giaBan
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("GioHangDB");
        }

        // Xóa sản phẩm khỏi giỏ hàng database
        [HttpPost]
        public async Task<IActionResult> RemoveFromCartDB(Guid idSanPhamChiTiet)
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }
            
            var gioHang = await _context.GioHang
                .Include(g => g.GioHangChiTiet)
                .FirstOrDefaultAsync(g => g.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (gioHang != null)
            {
                var chiTiet = gioHang.GioHangChiTiet
                    .FirstOrDefault(c => c.ID_SanPhamChiTiet == idSanPhamChiTiet);

                if (chiTiet != null)
                {
                    _context.GioHangChiTiet.Remove(chiTiet);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("GioHangDB");
        }
    }
}
