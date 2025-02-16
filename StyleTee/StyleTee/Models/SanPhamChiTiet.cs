using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class SanPhamChiTiet
    {
        [Key]
        public Guid ID_SanPhamChiTiet { get; set; }
        [Required(ErrorMessage = "ID sp là bắt buộc.")]
        public Guid ID_SanPham { get; set; }
        [Required(ErrorMessage = "ID màu là bắt buộc.")]
        public Guid ID_Mau { get; set; }
        [Required(ErrorMessage = "ID xuất xứ là bắt buộc.")]
        public Guid ID_XuatXu { get; set; }
        [Required(ErrorMessage = "ID thương hiệu là bắt buộc.")]
        public Guid ID_ThuongHieu { get; set; }
        [Required(ErrorMessage = "ID size là bắt buộc.")]
        public Guid ID_Size { get; set; }
        [Required(ErrorMessage = "ID danh mục là bắt buộc.")]
        public Guid ID_DanhMuc { get; set; }
        [Required(ErrorMessage = "ID chất liệu là bắt buộc.")]
        public Guid ID_ChatLieu { get; set; }
        [Required(ErrorMessage = "Gía là bắt buộc.")]
        public decimal Gia { get; set; }
        public ChatLieu ChatLieu { get; set; }
        public DanhMuc  DanhMuc { get; set; }
        public List<HinhAnh> HinhAnh { get; set; }
        public KichThuoc KichThuoc { get; set; }
        public MauSac MauSac { get; set; }
        public SanPham SanPham { get; set; }
        public ThuongHieu ThuongHieu { get; set; }
        public XuatXu XuatXu { get; set; }

    }
}
