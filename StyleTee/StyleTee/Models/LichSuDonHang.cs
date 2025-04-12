using System;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class LichSuDonHang
    {
        [Key]
        public Guid ID_LichSuDonHang { get; set; }

        [Required]
        public Guid ID_DonHang { get; set; }

        [Required]
        public string trangThai { get; set; }

        [Required]
        public DateTime ngayDatHang { get; set; }

        public string ghiChu { get; set; }

        // Navigation property
        public virtual DonHang DonHang { get; set; }
    }
} 