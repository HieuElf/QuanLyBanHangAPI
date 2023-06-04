using System;

namespace QuanLyBanHangAPI.Models.ChiTietDonHang
{
    public class ChiTietDonHangModel
    {
        public Guid MaDonHang { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string SoLuong { get; set; }
        public string DonGia { get; set; }
        public double ThanhTien { get; set; }
    }
}
