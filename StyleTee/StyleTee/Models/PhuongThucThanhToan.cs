using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class PhuongThucThanhToan
    {
        [Key]
        public Guid ID_PTTT { get; set; }
        public string  tenPhuongThuc { get; set; }
        public string  trangThai { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }

    }
}
