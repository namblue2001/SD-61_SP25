using StyleTee.Models.PhieuGiamGiaVaKhuyenMai;
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
        public Guid ID_ThongTinVanChuyen { get; set; }
        [Required]
       
        public Guid ID_MaGiamGia { get; set; }

        public decimal phiVanChuyen { get; set; }
        public string trangThaiDonHang { get; set; }
        public string trangThaiThanhToan { get; set; }
        public string phuongThucThanhToan { get; set; }
        public string ghiChu { get; set; }


        // Navigation properties
        //public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual ICollection<LichSuDonHang> LichSuDonHang { get; set; }
        public virtual ICollection<YeuCauDoiTra> YeuCauDoiTra { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ThongTinVanChuyen ThongTinVanChuyen { get; set; }
        public virtual PhieuGiamGia PhieuGiamGia { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
} 