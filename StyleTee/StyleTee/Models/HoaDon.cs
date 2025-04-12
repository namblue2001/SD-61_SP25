using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class HoaDon
    {
        [Key]
        public Guid ID_HoaDon { get; set; }
        public Guid ID_DonHang { get; set; }
        public string trangThai { get; set; }
        public DateTime ngayTao { get; set; }
        public decimal tongTien { get; set; }
        public DateTime ngayVanChuyen { get; set; }
        public DateTime ngayNhanHang { get; set; }
        public virtual DonHang DonHang { get; set; }
    }
}
