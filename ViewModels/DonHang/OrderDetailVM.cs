namespace WebBanGiayTheThao.ViewModels.DonHang
{
    public class OrderDetailVM
    {
        public string OrderCode { get; set; } = "";
        public DateTime OrderDate { get; set; }

        public string Receiver { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";

        public decimal Shipping { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public string Status { get; set; } = "";
    }

}
