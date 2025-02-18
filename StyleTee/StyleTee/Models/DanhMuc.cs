using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class DanhMuc
    {
        [Key]
        public Guid ID_DanhMuc { get; set; }
        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        public string tenDanhMuc { get; set; }
        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public bool trangThai { get; set; }
        public List<SanPhamChiTiet> SanPham { get; set; }
    }
}
