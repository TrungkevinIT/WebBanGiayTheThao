namespace WebBanGiayTheThao.ViewModels.DonHang
{
    public class OrderHistoryVM
    {
        public int OrderId { get; set; } 
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
