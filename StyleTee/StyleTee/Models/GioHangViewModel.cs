namespace StyleTee.Models
{
    public class GioHangViewModel
    {
        public List<GioHangChiTietViewModel> ChiTietGioHang { get; set; } = new List<GioHangChiTietViewModel>();
        public decimal TongTien => ChiTietGioHang.Sum(x => x.ThanhTien);
    }

    public class GioHangChiTietViewModel
    {
        public Guid ID_SanPhamChiTiet { get; set; }
        public string TenSanPham { get; set; }
        public string AnhDaiDien { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
    }

}
