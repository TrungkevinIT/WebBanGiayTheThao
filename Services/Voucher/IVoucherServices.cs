using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IVoucherServices
    {
        Task<List<Voucher>> GetAllAsync();
        Task CreateAsync (Voucher voucher);
        Task<bool> CheckTonTaiAsync(string maCode, int? idExclude = null);
    }
}
