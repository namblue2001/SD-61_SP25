using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class ThuongHieu
    {
        [Key]
        public Guid ID_ThuongHieu { get; set; }
        [Required(ErrorMessage = "Tên thương hiệu là bắt buộc.")]
        public string tenThuongHieu { get; set; }
        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public bool trangThai { get; set; }
        public List<SanPhamChiTiet> SanPham { get; set; }
    }
}
