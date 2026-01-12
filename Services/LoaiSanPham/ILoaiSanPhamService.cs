using WebBanGiayTheThao.Models;
﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebBanGiayTheThao.Services
{
    public interface ILoaiSanPhamService
    {
        Task<IEnumerable<LoaiSanPham>> GetAllLoaiSanPhamAsync(string searchName, int? trangThai);
        Task<LoaiSanPham?> GetLoaiSanPhamByIdAsync(int id);

        Task CapNhatLoaiSanPhamAsync(LoaiSanPham loaiSanPham);
        Task<bool> CapNhatTrangThaiAsync(int id, int trangThai);
        Task ThemLoaiSanPhamAsync(LoaiSanPham loaiSanPham);
    }
}