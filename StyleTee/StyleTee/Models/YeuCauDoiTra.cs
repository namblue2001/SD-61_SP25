using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class YeuCauDoiTra
    {
        [Key]
        public Guid ID_YeuCauDoiTra { get; set; }
        public Guid ID_DonHang { get; set; }
        public string loaiYeuCau { get; set; }
        public string liDoDoiTra { get; set; }
        public virtual ICollection<AnhMinhChung> AnhMinhChung { get; set; }
        public virtual ICollection<SanPhamDoiTra> SanPhamDoiTra { get; set; }
        public virtual DonHang DonHang { get; set; }
    }
}
