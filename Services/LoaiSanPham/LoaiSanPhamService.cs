using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;
using WebBanGiayTheThao.ViewModels;

namespace WebBanGiayTheThao.Services
{
    public class LoaiSanPhamService : ILoaiSanPhamService
    {
        private readonly QuanLyWebBanGiayContext _context; 
        public LoaiSanPhamService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }
        public async Task<List<WebBanGiayTheThao.Models.LoaiSanPham>> LoadDSLoaiSanPham()
        {
            return await _context.LoaiSanPhams.Where(x => x.TrangThai == 1).OrderBy(x => x.TenLoai).ToListAsync();
        }
        public async Task<LoaiSanPham?> ThemLoaiNhanh(string ten)
        {
            if(await _context.LoaiSanPhams.AnyAsync(x => x.TenLoai == ten))
            {
                return null; 
            }
            var loai = new LoaiSanPham { TenLoai = ten , TrangThai=1};
            _context.LoaiSanPhams.Add(loai);
            await _context.SaveChangesAsync();
            return loai;
        }
        public async Task<IEnumerable<LoaiSanPham>> GetAllLoaiSanPhamAsync(string searchName, int? trangThai)
        {

            var query = _context.LoaiSanPhams.AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(x => x.TenLoai.Contains(searchName));
            }
            if (trangThai.HasValue)
            {
                query = query.Where(x => x.TrangThai == trangThai.Value);
            }
            return await query.OrderBy(x => x.Id).ToListAsync();
        }
        public async Task<LoaiSanPham?> GetLoaiSanPhamByIdAsync(int id)
        {
            return await _context.LoaiSanPhams.FindAsync(id);
        }
        public async Task CapNhatLoaiSanPhamAsync(LoaiSanPham loaiSanPham)
        {
            try
            {
                var existingItem = await _context.LoaiSanPhams.FindAsync(loaiSanPham.Id);
                if (existingItem != null)
                {
                    existingItem.TenLoai = loaiSanPham.TenLoai;
                    existingItem.TrangThai = loaiSanPham.TrangThai;
                    _context.LoaiSanPhams.Update(existingItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật: " + ex.Message);
            }
        }
        public async Task<bool> CapNhatTrangThaiAsync(int id, int trangThai)
        {
            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPham == null) return false;

            loaiSanPham.TrangThai = trangThai;
            _context.Update(loaiSanPham);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task ThemLoaiSanPhamAsync(LoaiSanPham loaiSanPham)
        {

                loaiSanPham.TenLoai = loaiSanPham.TenLoai.Trim();
                bool daTonTai = await _context.LoaiSanPhams
                    .AnyAsync(x => x.TenLoai.ToLower() == loaiSanPham.TenLoai.ToLower());

                if (daTonTai)
                {
                    throw new Exception($"Tên loại sản phẩm '{loaiSanPham.TenLoai}' đã tồn tại trong hệ thống!");
                }
                await _context.LoaiSanPhams.AddAsync(loaiSanPham);
                await _context.SaveChangesAsync();
            
        }
        //trung
        public async Task<List<LoaiSanPhamVM>> GetDanhMucNoiBatAsync()
        {
            return await _context.LoaiSanPhams
                .Where(loai => loai.TrangThai == 1 && loai.SanPhams.Any(sp => sp.TrangThai == 1))
                .Select(loai => new LoaiSanPhamVM
                {
                    Id = loai.Id,
                    TenLoai = loai.TenLoai,
                    AnhHienThi = loai.SanPhams
                                     .Where(sp => sp.TrangThai == 1)
                                     .OrderByDescending(sp => sp.Id)
                                     .Select(sp => sp.AnhDaiDien)
                                     .FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}





