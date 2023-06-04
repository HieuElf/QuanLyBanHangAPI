using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace QuanLyBanHangAPI.Models.ChiTietDonHang
{
    public class ChiTietDonHangVM
    {
        public Guid MaChiTietDonHang { get; set; }
        public Guid MaDonHang { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string SoLuong { get; set; }
        public string DonGia { get; set; }
        public double ThanhTien { get; set; }
    }
}
