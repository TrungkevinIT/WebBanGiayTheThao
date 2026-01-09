using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IVoucherServices
    {
        Task<List<Voucher>> GetAllAsync();
    }
}
