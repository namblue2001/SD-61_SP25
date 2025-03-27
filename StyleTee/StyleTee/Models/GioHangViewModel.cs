namespace StyleTee.Models
{
    public class GioHangViewModel
    {
        public List<GioHangChiTietViewModel> ChiTietGioHang { get; set; } = new();
        public decimal TongTien => ChiTietGioHang.Sum(x => x.ThanhTien);
    }

    public class GioHangChiTietViewModel
    {
        public Guid ID_SanPhamChiTiet { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string AnhDaiDien { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }

        // Tính tổng tiền & làm tròn 2 chữ số thập phân
        public decimal ThanhTien => decimal.Round(DonGia * SoLuong, 2);
    }

}
