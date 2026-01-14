using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.ViewModels.BinhLuan
{
    public class BinhLuanAdminVM
    {
        public List<Models.BinhLuan> DanhSach { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int? TrangThai { get; set; }
    }
}
