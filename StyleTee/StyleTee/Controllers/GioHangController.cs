using Microsoft.AspNetCore.Mvc;
using StyleTee.Models;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using Microsoft.Extensions.Logging;
using StyleTee.Services;

namespace StyleTee.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GioHangController> _logger;
        private readonly GHNService _ghnService;

        public GioHangController(ApplicationDbContext context, ILogger<GioHangController> logger, GHNService ghnService)
        {
            _context = context;
            _logger = logger;
            _ghnService = ghnService;
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

            // Lấy danh sách tỉnh/thành phố từ GHN
            var provinces = await _ghnService.GetProvinces();
            ViewBag.Provinces = provinces;

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

            // Lấy district_id từ GHN API
            int? districtId = null;
            if (diaChiMacDinh != null && !string.IsNullOrEmpty(diaChiMacDinh.huyen))
            {
                districtId = await _ghnService.GetDistrictIdByName(diaChiMacDinh.huyen);
            }

            // Lưu district_id vào ViewBag
            ViewBag.DistrictId = districtId;

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
        public async Task<IActionResult> DatHang([FromBody] DonHang donHang)
        {
            try
            {
                var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
                if (string.IsNullOrEmpty(idTaiKhoan))
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập để đặt hàng" });
                }

                // Lấy giỏ hàng từ database
                var gioHang = await _context.GioHang
                    .Include(g => g.GioHangChiTiet)
                    .FirstOrDefaultAsync(g => g.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

                if (gioHang == null || !gioHang.GioHangChiTiet.Any())
                {
                    return Json(new { success = false, message = "Giỏ hàng trống" });
                }

                // Tạo đơn hàng mới
                donHang.ID_DonHang = Guid.NewGuid();
                donHang.ID_TaiKhoan = Guid.Parse(idTaiKhoan);
                donHang.ngayDatHang = DateTime.Now;
                donHang.trangThaiDonHang = "Chờ xử lý";
                donHang.trangThaiThanhToan = "Chưa thanh toán";

                // Tính tổng tiền đơn hàng
                donHang.tongTien = gioHang.GioHangChiTiet.Sum(item => item.donGia * item.soLuong);

                // Lưu đơn hàng vào database
                _context.DonHang.Add(donHang);
                await _context.SaveChangesAsync();

                // Lưu chi tiết đơn hàng
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

                await _context.SaveChangesAsync();

                // Xóa giỏ hàng
                _context.GioHangChiTiet.RemoveRange(gioHang.GioHangChiTiet);
                _context.GioHang.Remove(gioHang);
                await _context.SaveChangesAsync();

                return Json(new { success = true, donHangId = donHang.ID_DonHang });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đặt hàng");
                return Json(new { success = false, message = "Có lỗi xảy ra khi đặt hàng" });
            }
        }

        public async Task<IActionResult> DatHangThanhCong()
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }
            try
            {
                
                var donHang = await _context.DonHang
                    .Include(d => d.ChiTietDonHang)
                        .ThenInclude(ct => ct.SanPhamChiTiet)
                            .ThenInclude(spct => spct.SanPham)
                    .Include(d => d.ChiTietDonHang)
                        .ThenInclude(ct => ct.SanPhamChiTiet)
                            .ThenInclude(spct => spct.KichThuoc)
                    .Include(d => d.ChiTietDonHang)
                        .ThenInclude(ct => ct.SanPhamChiTiet)
                            .ThenInclude(spct => spct.MauSac)
                    .Include(d => d.ChiTietDonHang)
                        .ThenInclude(ct => ct.SanPhamChiTiet)
                            .ThenInclude(spct => spct.HinhAnh)
                    .FirstOrDefaultAsync(d => d.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

                if (donHang == null)
                {
                    _logger.LogWarning($"Không tìm thấy đơn hàng với ID: {idTaiKhoan}");
                    return RedirectToAction("Index");
                }

                return View(donHang);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi tải đơn hàng {idTaiKhoan}");
                return RedirectToAction("Index");
            }
        }

        public IActionResult XemDonHang(Guid id)
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }

            // Kiểm tra xem có đơn hàng nào không
            var donHangExists = _context.DonHang.Any(d => d.ID_DonHang == id);
            if (!donHangExists)
            {
                _logger.LogWarning($"Không tìm thấy đơn hàng với ID: {id}");
                return RedirectToAction("Index");
            }

            // Kiểm tra xem đơn hàng có thuộc về tài khoản này không
            var donHangBelongsToUser = _context.DonHang.Any(d => 
                d.ID_DonHang == id && 
                d.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (!donHangBelongsToUser)
            {
                _logger.LogWarning($"Đơn hàng {id} không thuộc về tài khoản {idTaiKhoan}");
                return RedirectToAction("Index");
            }

            var donHang = _context.DonHang
                .Include(d => d.ChiTietDonHang)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(spct => spct.SanPham)
                .Include(d => d.ChiTietDonHang)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(spct => spct.KichThuoc)
                .Include(d => d.ChiTietDonHang)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(spct => spct.MauSac)
                .Include(d => d.ChiTietDonHang)
                    .ThenInclude(ct => ct.SanPhamChiTiet)
                        .ThenInclude(spct => spct.HinhAnh)
                .FirstOrDefault(d => d.ID_DonHang == id && d.ID_TaiKhoan == Guid.Parse(idTaiKhoan));

            if (donHang == null)
            {
                return RedirectToAction("Index");
            }

            return View(donHang);
        }

        // Hiển thị danh sách đơn hàng theo ID_TaiKhoan
        public async Task<IActionResult> DanhSachDonHang()
        {
            var idTaiKhoan = HttpContext.Session.GetString("id_taikhoan");
            if (string.IsNullOrEmpty(idTaiKhoan))
            {
                return RedirectToAction("DangNhap", "Access");
            }

            try
            {
                var danhSachDonHang = await _context.DonHang
                    .Include(d => d.ChiTietDonHang)
                        .ThenInclude(ct => ct.SanPhamChiTiet)
                            .ThenInclude(spct => spct.SanPham)
                    .Include(d => d.ChiTietDonHang)
                        .ThenInclude(ct => ct.SanPhamChiTiet)
                            .ThenInclude(spct => spct.HinhAnh)
                    .Where(d => d.ID_TaiKhoan == Guid.Parse(idTaiKhoan))
                    .OrderByDescending(d => d.ngayDatHang)
                    .ToListAsync();

                return View(danhSachDonHang);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi tải danh sách đơn hàng cho tài khoản {idTaiKhoan}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetDistricts(int provinceId)
        {
            var districts = await _ghnService.GetDistrictsByProvinceId(provinceId);
            return Json(new { success = true, districts = districts });
        }

        [HttpPost]
        public async Task<IActionResult> CalculateShippingFee(int districtId)
        {
            var fee = await _ghnService.CalculateShippingFee(districtId);
            return Json(new { success = true, fee = fee });
        }

        public class DistrictRequest
        {
            public string districtName { get; set; }
        }
    }
}
