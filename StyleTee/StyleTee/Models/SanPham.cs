using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleTee.Models
{
    public class SanPham
    {
        [Key]
       
        public Guid ID_SanPham { get; set; }
        public Guid ID_DanhMuc { get; set; }
        public string tenSanPham { get; set; }
        public string moTa { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal giaGoc { get; set; }
        public bool trangThai { get; set; }
        public DanhMuc DanhMuc { get; set; }
        public DateTime ngayTao {get;set;}
        public string anhDaiDien {get;set;}
        public ICollection<SanPhamChiTiet> SanPhamChiTiet { get; set; }
    }
}
