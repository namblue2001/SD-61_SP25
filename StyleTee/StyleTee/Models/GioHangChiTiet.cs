using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class GioHangChiTiet
    {
        [Key]
        public Guid ID_GioHangChiTiet { get; set; }
        public Guid ID_GioHang { get; set; }
        public Guid ID_SanPhamChiTiet { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal donGia { get; set; }
        public int soLuong { get; set; }
        // hiển thị màu sắc, size, xuất xứ, chất liệu, kiểu dáng
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
        public virtual GioHang GioHang { get; set; }
    }
}
