using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class GioHang
    {
        [Key]
        public Guid ID_GioHang { get; set; }
        public Guid ID_TaiKhoan { get; set; }
        public DateTime ngayTao { get; set; } = DateTime.Now; // Gán giá trị mặc định
        public TaiKhoan TaiKhoan { get; set; }
        public ICollection<GioHangChiTiet> GioHangChiTiet { get; set; }

    }
}
