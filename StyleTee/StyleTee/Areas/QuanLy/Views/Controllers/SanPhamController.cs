using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;
using StyleTee.Models.SanPhamVaThuocTinh;

namespace StyleTee.Areas.QuanLy.Controllers.SanPhamVaThuocTinh
{
    [Area("QuanLy")]
    [Route("QuanLy")]
    [Route("QuanLy/SanPham")]
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SanPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        
        public void Trang()
        {
            TempData["Trang"] = "Quản lý sản phẩm";
        }

        private bool SanPhamExists(Guid id)
        {
            return (_context.SanPham?.Any(e => e.ID_SanPham == id)).GetValueOrDefault();
        }
    }
}
