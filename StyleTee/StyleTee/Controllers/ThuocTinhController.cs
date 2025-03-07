using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;

namespace StyleTee.Controllers
{

    public class ThuocTinhController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThuocTinhController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? type, string? title)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                {
                    return View();
                }

                object data;
                switch (type)
                {
                    case "ChatLieu":
                        data = _context.ChatLieu.Select(x => new { x.tenChatLieu, x.trangThai, x.ID_ChatLieu }).ToList();
                        break;
                    case "MauSac":
                        data = _context.MauSac.Select(x => new { x.tenMauSac, x.trangThai, x.ID_MauSac }).ToList();
                        break;
                    case "XuatXu":
                        data = _context.XuatXu.Select(x => new { x.tenXuatXu, x.trangThai, x.ID_XuatXu }).ToList();
                        break;
                    case "DanhMuc":
                        data = _context.DanhMuc.Select(x => new { x.tenDanhMuc, x.trangThai, x.ID_DanhMuc }).ToList();
                        break;
                    case "ThuongHieu":
                        data = _context.ThuongHieu.Select(x => new { x.tenThuongHieu, x.trangThai, x.ID_ThuongHieu }).ToList();
                        break;
                    case "KichThuoc":
                        data = _context.KichThuoc.Select(x => new { x.tenKichThuoc, x.trangThai, x.ID_KichThuoc }).ToList();
                        break;
                    default:
                        return NotFound("Không tìm thấy thuộc tính.");
                }

                return PartialView("_DanhSachThuocTinh", data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }



        // Thêm mới Chất liệu - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateChatLieu([FromForm] string tenChatLieu, [FromForm] bool trangThai)
        {
            if (ModelState.IsValid)
            {
                var chatLieu = new ChatLieu
                {
                    ID_ChatLieu = Guid.NewGuid(),
                    tenChatLieu = tenChatLieu,
                    trangThai = trangThai
                };
                _context.ChatLieu.Add(chatLieu);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // Thêm mới Màu sắc - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMauSac([FromForm] string tenMauSac, [FromForm] bool trangThai)
        {
            if (ModelState.IsValid)
            {
                var mauSac = new MauSac
                {
                    ID_MauSac = Guid.NewGuid(),
                    tenMauSac = tenMauSac,
                    trangThai = trangThai
                };
                _context.MauSac.Add(mauSac);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // Thêm mới Xuất xứ - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateXuatXu([FromForm] string tenXuatXu, [FromForm] bool trangThai)
        {
            if (ModelState.IsValid)
            {
                var xuatXu = new XuatXu
                {
                    ID_XuatXu = Guid.NewGuid(),
                    tenXuatXu = tenXuatXu,
                    trangThai = trangThai
                };
                _context.XuatXu.Add(xuatXu);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // Thêm mới Danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDanhMuc([FromForm] string tenDanhMuc, [FromForm] bool trangThai)
        {
            if (ModelState.IsValid)
            {
                var danhMuc = new DanhMuc
                {
                    ID_DanhMuc = Guid.NewGuid(),
                    tenDanhMuc = tenDanhMuc,
                    trangThai = trangThai
                };
                _context.DanhMuc.Add(danhMuc);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // Thêm mới Thương hiệu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateThuongHieu([FromForm] string tenThuongHieu, [FromForm] bool trangThai)
        {
            if (ModelState.IsValid)
            {
                var thuongHieu = new ThuongHieu
                {
                    ID_ThuongHieu = Guid.NewGuid(),
                    tenThuongHieu = tenThuongHieu,
                    trangThai = trangThai
                };
                _context.ThuongHieu.Add(thuongHieu);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // Thêm mới Kích thước
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKichThuoc([FromForm] string tenKichThuoc, [FromForm] bool trangThai)
        {
            if (ModelState.IsValid)
            {
                var kichThuoc = new KichThuoc
                {
                    ID_KichThuoc = Guid.NewGuid(),
                    tenKichThuoc = tenKichThuoc,
                    trangThai = trangThai,
                    moTa = "" // Thêm mô tả mặc định vì field này là required
                };
                _context.KichThuoc.Add(kichThuoc);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid id, string type)
        {
            switch (type)
            {
                case "ChatLieu":
                    var chatLieu = await _context.ChatLieu.FindAsync(id);
                    if (chatLieu == null) return NotFound();
                    return View("EditChatLieu", chatLieu);
                
                case "MauSac":
                    var mauSac = await _context.MauSac.FindAsync(id);
                    if (mauSac == null) return NotFound();
                    return View("EditMauSac", mauSac);
                
                case "XuatXu":
                    var xuatXu = await _context.XuatXu.FindAsync(id);
                    if (xuatXu == null) return NotFound();
                    return View("EditXuatXu", xuatXu);
                
                default:
                    return NotFound();
            }
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string type, object model)
        {
            try
            {
                switch (type)
                {
                    case "ChatLieu":
                        var chatLieu = (ChatLieu)model;
                        if (id != chatLieu.ID_ChatLieu) return NotFound();
                        _context.Update(chatLieu);
                        break;
                    
                    case "MauSac":
                        var mauSac = (MauSac)model;
                        if (id != mauSac.ID_MauSac) return NotFound();
                        _context.Update(mauSac);
                        break;
                    
                    case "XuatXu":
                        var xuatXu = (XuatXu)model;
                        if (id != xuatXu.ID_XuatXu) return NotFound();
                        _context.Update(xuatXu);
                        break;
                    
                    default:
                        return NotFound();
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật. Vui lòng thử lại.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChatLieu(Guid id, [FromForm] string tenChatLieu, [FromForm] bool trangThai)
        {
            try
            {
                var chatLieu = await _context.ChatLieu.FindAsync(id);
                if (chatLieu == null) return Json(new { success = false, errors = new[] { new { errorMessage = "Không tìm thấy chất liệu." } } });

                chatLieu.tenChatLieu = tenChatLieu;
                chatLieu.trangThai = trangThai;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { new { errorMessage = ex.Message } } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMauSac(Guid id, [FromForm] string tenMauSac, [FromForm] bool trangThai)
        {
            try
            {
                var mauSac = await _context.MauSac.FindAsync(id);
                if (mauSac == null) return Json(new { success = false, errors = new[] { new { errorMessage = "Không tìm thấy màu sắc." } } });

                mauSac.tenMauSac = tenMauSac;
                mauSac.trangThai = trangThai;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { new { errorMessage = ex.Message } } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditXuatXu(Guid id, [FromForm] string tenXuatXu, [FromForm] bool trangThai)
        {
            try
            {
                var xuatXu = await _context.XuatXu.FindAsync(id);
                if (xuatXu == null) return Json(new { success = false, errors = new[] { new { errorMessage = "Không tìm thấy xuất xứ." } } });

                xuatXu.tenXuatXu = tenXuatXu;
                xuatXu.trangThai = trangThai;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { new { errorMessage = ex.Message } } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDanhMuc(Guid id, [FromForm] string tenDanhMuc, [FromForm] bool trangThai)
        {
            try
            {
                var danhMuc = await _context.DanhMuc.FindAsync(id);
                if (danhMuc == null) return Json(new { success = false, errors = new[] { new { errorMessage = "Không tìm thấy danh mục." } } });

                danhMuc.tenDanhMuc = tenDanhMuc;
                danhMuc.trangThai = trangThai;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { new { errorMessage = ex.Message } } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditThuongHieu(Guid id, [FromForm] string tenThuongHieu, [FromForm] bool trangThai)
        {
            try
            {
                var thuongHieu = await _context.ThuongHieu.FindAsync(id);
                if (thuongHieu == null) return Json(new { success = false, errors = new[] { new { errorMessage = "Không tìm thấy thương hiệu." } } });

                thuongHieu.tenThuongHieu = tenThuongHieu;
                thuongHieu.trangThai = trangThai;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { new { errorMessage = ex.Message } } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditKichThuoc(Guid id, [FromForm] string tenKichThuoc, [FromForm] bool trangThai)
        {
            try
            {
                var kichThuoc = await _context.KichThuoc.FindAsync(id);
                if (kichThuoc == null) return Json(new { success = false, errors = new[] { new { errorMessage = "Không tìm thấy kích thước." } } });

                kichThuoc.tenKichThuoc = tenKichThuoc;
                kichThuoc.trangThai = trangThai;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { new { errorMessage = ex.Message } } });
            }
        }

        private bool SanPhamExists(Guid id)
        {
            return _context.ChatLieu.Any(e => e.ID_ChatLieu == id);
        }
    }
}
