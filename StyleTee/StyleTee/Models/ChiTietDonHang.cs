using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public Guid ID_DonHangChiTiet { get; set; }

        [Required]
        public Guid ID_DonHang { get; set; }

        [Required]
        public Guid ID_SanPhamChiTiet { get; set; }

        [Required]
        public int soLuong { get; set; }

        [Required]
        public decimal donGia { get; set; }

        [Required]
        public decimal tongTien { get; set; }

        // Navigation properties
        public virtual DonHang DonHang { get; set; }
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
    }
} 