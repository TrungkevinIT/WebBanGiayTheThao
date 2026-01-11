namespace WebBanGiayTheThao.ViewModels.BaoCao
{
    public class BaoCaoDoanhThuVM
    {
        public int Thang { get; set; }
        public int Nam { get; set; }

        public decimal TongDoanhThu { get; set; }

        public List<DoanhThuTheoNgayVM> ChiTietTheoNgay { get; set; }
            = new();
    }


}
