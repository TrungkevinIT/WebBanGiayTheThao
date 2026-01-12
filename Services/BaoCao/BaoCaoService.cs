using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Chỉ tính hóa đơn hoàn thành (TrangThai = 1)
        /// </summary>
        public BaoCaoDoanhThuVM ThongKeDoanhThuTheoThang(int thang, int nam)
        {
            var vm = new BaoCaoDoanhThuVM
            {
                Thang = thang,
                Nam = nam,

                ChiTietTheoNgay = ChiTietTheoNgay(thang, nam),
                TongDoanhThu = TongDoanhThuTheoThang(thang, nam),

                Top5SanPham = Top5SanPhamBanChay(thang, nam),
                DoanhThu12Thang = DoanhThuTheoNam(nam)
            };

            return vm;
        }

        // =====================================================
        // DOANH THU THEO NGÀY
        // =====================================================
        public List<DoanhThuTheoNgayVM> ChiTietTheoNgay(int thang, int nam)
        {
            return _context.HoaDons
                .Where(hd => hd.NgayDat.HasValue
                             && hd.NgayDat.Value.Month == thang
                             && hd.NgayDat.Value.Year == nam
                             && hd.TrangThai == 1)
                .GroupBy(hd => hd.NgayDat!.Value.Date)
                .Select(g => new DoanhThuTheoNgayVM
                {
                    Ngay = g.Key,
                    TongTien = g.Sum(x => x.TongTien ?? 0)
                })
                .OrderBy(x => x.Ngay)
                .ToList();
        }

        // =====================================================
        // TỔNG DOANH THU THEO THÁNG
        // =====================================================
        public decimal TongDoanhThuTheoThang(int thang, int nam)
        {
            return _context.HoaDons
                .Where(hd => hd.NgayDat.HasValue
                             && hd.NgayDat.Value.Month == thang
                             && hd.NgayDat.Value.Year == nam
                             && hd.TrangThai == 1)
                .Sum(hd => hd.TongTien ?? 0);
        }

        // =====================================================
        // TOP 5 SẢN PHẨM BÁN CHẠY
        // =====================================================
        public List<TopSanPhamBanChayVM> Top5SanPhamBanChay(int thang, int nam)
        {
            var query = from ct in _context.CthoaDons
                        join hd in _context.HoaDons on ct.HoaDonId equals hd.Id
                        join sp in _context.SanPhams on ct.SanPhamId equals sp.Id
                        where hd.NgayDat.HasValue
                              && hd.NgayDat.Value.Month == thang
                              && hd.NgayDat.Value.Year == nam
                              && hd.TrangThai == 1
                        group new { ct, sp } by new
                        {
                            sp.Id,
                            sp.TenSanPham
                        }
                        into g
                        select new TopSanPhamBanChayVM
                        {
                            SanPhamId = g.Key.Id,
                            TenSanPham = g.Key.TenSanPham,
                            TongSoLuongBan = g.Sum(x => x.ct.SoLuong),
                            TongDoanhThu = g.Sum(x =>
                                (x.ct.DonGia ?? 0) * x.ct.SoLuong)
                        };

            return query
                .OrderByDescending(x => x.TongSoLuongBan)
                .Take(5)
                .ToList();
        }

        // =====================================================
        // DOANH THU 12 THÁNG TRONG NĂM
        // =====================================================
        public List<DoanhThuTheoThangVM> DoanhThuTheoNam(int nam)
        {
            var data = _context.HoaDons
                .Where(hd => hd.NgayDat.HasValue
                             && hd.NgayDat.Value.Year == nam
                             && hd.TrangThai == 1)
                .GroupBy(hd => hd.NgayDat!.Value.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    Tong = g.Sum(x => x.TongTien ?? 0)
                })
                .ToList();

            var result = new List<DoanhThuTheoThangVM>();
            for (int i = 1; i <= 12; i++)
            {
                var item = data.FirstOrDefault(x => x.Thang == i);
                result.Add(new DoanhThuTheoThangVM
                {
                    Thang = i,
                    TongDoanhThu = item?.Tong ?? 0
                });
            }

            return result;
        }
    }
}
