namespace WebBanGiayTheThao.ViewModels.DonHang
{
    public class OrderProductVM
    {
        public int SanPhamId { get; set; }
        public string TenSanPham { get; set; } = "";
        public double Size { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string Anh { get; set; } = "";
    }
}
