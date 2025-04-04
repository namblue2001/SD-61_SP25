using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class ThongTinVanChuyen
    {
        [Key]
        public Guid ID_VanChuyen { get; set; }

        public string tenNguoiNhan { get; set; } 
        
        public string soDienThoai { get; set; }
        public string xa { get; set; }
        public string huyen { get; set; }
        public string tinh { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }
    }
}
