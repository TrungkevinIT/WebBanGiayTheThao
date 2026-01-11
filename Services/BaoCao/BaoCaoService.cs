using System;
using System.Linq;
using WebBanGiayTheThao.Common;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.ViewModels.BaoCao;

namespace WebBanGiayTheThao.Services
{
    public class BaoCaoService
    {
        private readonly QuanLyWebBanGiayContext _context;

        public BaoCaoService(QuanLyWebBanGiayContext context)
        {
            _context = context;
        }

        /// <summary>
        /// UC049 - Thống kê doanh thu theo tháng
        /// Chỉ tính hóa đơn Hoàn thành (TrangThai = 1)
        /// </summary>
        public BaoCaoDoanhThuVM ThongKeDoanhThuTheoThang(int thang, int nam)
        {
            // Lấy các hóa đơn HOÀN THÀNH trong tháng/năm
            var hoaDonHoanThanh = _context.HoaDons
                .Where(hd =>
                    hd.TrangThai == TrangThaiHoaDon.HoanThanh &&
                    hd.NgayDat.HasValue &&
                    hd.NgayDat.Value.Month == thang &&
                    hd.NgayDat.Value.Year == nam
                );

            // Tổng doanh thu
            decimal tongDoanhThu = hoaDonHoanThanh
                .Sum(hd => (decimal?)hd.TongTien) ?? 0;

            // Chi tiết doanh thu theo ngày
            var chiTietTheoNgay = hoaDonHoanThanh
                .GroupBy(hd => hd.NgayDat!.Value.Date)
                .Select(g => new DoanhThuTheoNgayVM
                {
                    Ngay = g.Key,
                    TongTien = g.Sum(x => x.TongTien ?? 0)
                })
                .OrderBy(x => x.Ngay)
                .ToList();

            // Trả ViewModel
            return new BaoCaoDoanhThuVM
            {
                Thang = thang,
                Nam = nam,
                TongDoanhThu = tongDoanhThu,
                ChiTietTheoNgay = chiTietTheoNgay
            };
        }
    }
}
