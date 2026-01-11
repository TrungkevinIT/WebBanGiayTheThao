using System.Collections.Generic;
using System.Threading.Tasks;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public interface ILoaiSanPhamService
    {
        Task<IEnumerable<LoaiSanPham>> GetAllLoaiSanPhamAsync(string searchName, int? trangThai);
       
    }
}