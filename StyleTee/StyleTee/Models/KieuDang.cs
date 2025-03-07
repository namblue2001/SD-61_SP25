using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class KieuDang
    {

        [Key]
        public Guid ID_KieuDang { get; set; }
        [Required(ErrorMessage = "Tên kiểu dáng là bắt buộc.")]
        public string tenKieuDang { get; set; }
        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public bool trangThai { get; set; }
        public ICollection<SanPhamChiTiet> SanPhamChiTiet { get; set; }
    }
}
