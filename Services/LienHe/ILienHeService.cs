using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface ILienHeService
    {
        Task AddAsync(LienHe lienhe);
        Task<List<LienHe>> GetAllLienHe();
    }
}
