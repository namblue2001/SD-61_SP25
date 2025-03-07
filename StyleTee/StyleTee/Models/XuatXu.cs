using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class XuatXu
    {
        [Key]
        public Guid ID_XuatXu { get; set; }
        [Required(ErrorMessage = "Tên xuất xứ là bắt buộc.")]
        public string tenXuatXu { get; set; }
        [RegularExpression("^(true|false)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public bool trangThai { get; set; }
        public ICollection<SanPhamChiTiet> SanPhamChiTiets { get; set; }
    }
}
