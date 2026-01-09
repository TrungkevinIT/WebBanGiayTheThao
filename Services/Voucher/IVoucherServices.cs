using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IVoucherServices
    {
        Task<List<Voucher>> GetAllAsync();
        Task CreateAsync (Voucher voucher);
        Task<Voucher?> GetByIdAsync(int id);
        Task UpdateAsync (Voucher voucher);
        Task<bool> UpdateTrangThaiAsync(int id, int trangThai);
        Task<bool> CheckTonTaiAsync(string maCode, int? idExclude = null);

    }
}
