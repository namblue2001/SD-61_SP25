using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class MauSac
    {
        [Key]
        
        public Guid ID_MauSac { get; set; }
        [Required(ErrorMessage = "Tên màu sắc là bắt buộc.")]
        public string tenMauSac { get; set; }
        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public bool trangThai { get; set; }
        public List<SanPhamChiTiet> SanPham { get; set; }
    }
}
