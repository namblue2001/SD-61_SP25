using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class DonHang
    {
        [Key]
        public Guid ID_DonHang { get; set; }

        [Required]
        public Guid ID_TaiKhoan { get; set; }

        [Required]
        public DateTime ngayDatHang { get; set; }

        [Required]
        public decimal tongTien { get; set; }

        [Required]
        public string trangThaiDonHang { get; set; }

        [Required]
        public string diaChiVanChuyen { get; set; }

        [Required]
        public string trangThaiThanhToan { get; set; }

        [Required]
        public string phuongThucThanhToan { get; set; }

        // Navigation properties
        //public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual ICollection<LichSuDonHang> LichSuDonHang { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
} 