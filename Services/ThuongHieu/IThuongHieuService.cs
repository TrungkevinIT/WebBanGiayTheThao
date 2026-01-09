using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface IThuongHieuService
    {
        Task<List<ThuongHieu>> GetAllAsync(string? searchString);
        Task<ThuongHieu?> GetByIdAsync(int id);
        Task CreateAsync(ThuongHieu thuongHieu);
        Task UpdateAsync(ThuongHieu thuongHieu);
        Task<bool> UpdateTrangThaiAsync(int id, int trangThai);
        Task<bool> CheckTenTonTaiAsync(string tenThuongHieu, int? idExclude = null);
    }
}