using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface ILienHeService
    {
        Task AddAsync(LienHe lienhe);
        Task<List<LienHe>> GetAllLienHe();
        Task CapNhatTrangThaiAsync(int Id, int TrangThaiMoi);
        Task XoaLienHeAsync (int id);
    }
}
