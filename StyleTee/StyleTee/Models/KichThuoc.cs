using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class KichThuoc
    {
        [Key]
        
        public Guid ID_KichThuoc { get; set; }
        [Required(ErrorMessage = "Tên kích thước là bắt buộc.")]
        public string tenKichThuoc { get; set; }
        [Required(ErrorMessage = "Mô tả kích thước là bắt buộc.")]
        public string moTa { get; set; }
        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public bool trangThai { get; set; }
        public List<SanPhamChiTiet> SanPham { get; set; }
    }
}
