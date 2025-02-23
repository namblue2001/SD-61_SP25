using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class HinhAnh
    {
        [Key]
        public Guid ID_HinhAnh { get; set; }
        public Guid ID_SanPham { get; set; }
        public Guid ID_SanPhamChiTiet { get; set; }
        public string url_hinhAnh { get; set; }
        public SanPham SanPham { get; set; }
        public SanPhamChiTiet SanPhamChiTiet { get; set;}
    }
}
