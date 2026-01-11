using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Models;

namespace WebBanGiayTheThao.Services
{
    public class LoaiSanPhamService : ILoaiSanPhamService
    {
        private readonly QuanLyWebBanGiayContext _context;
        public LoaiSanPhamService(QuanLyWebBanGiayContext context)
        {
            _context = context;
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
            try
            {

                bool daTonTai = await _context.LoaiSanPhams
                    .AnyAsync(x => x.TenLoai == loaiSanPham.TenLoai);

                if (daTonTai)
                {
                    throw new Exception("Tên loại sản phẩm này đã tồn tại trong hệ thống!");
                }
                await _context.LoaiSanPhams.AddAsync(loaiSanPham);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi khi thêm loại sản phẩm: " + ex.Message);
            }
        }

    }
}