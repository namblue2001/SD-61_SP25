using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class SanPhamDoiTra
    {
        [Key]
        public Guid ID_SPDoiTra { get; set; }
        public Guid ID_ChiTietDonHang { get; set; }
        public Guid ID_YeuCauDoiTra { get; set; }
        public int soLuong { get; set; }
        public virtual ChiTietDonHang ChiTietDonHang { get;set; }
        public virtual YeuCauDoiTra YeuCauDoiTra { get;set; }

    }
}
