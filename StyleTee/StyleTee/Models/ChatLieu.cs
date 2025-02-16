using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class ChatLieu
    {
        [Key]
        public Guid ID_ChatLieu { get; set; }
        [Required(ErrorMessage = "Tên chất liệu là bắt buộc.")]
        public string tenChatLieu { get; set; }
        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public bool trangThai { get; set; }
        public List<SanPhamChiTiet>   SanPham { get; set; }

    }
}
