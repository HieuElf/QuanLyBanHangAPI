using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("ChiTietDonHang")]
    public class ChiTietDonHang
    {
        [Key]
        public Guid MaChiTietDonHang { get; set; }
        public Guid MaDonHang { get; set; }
        [ForeignKey("MaDonHang")]
        public DonDatHang DonDatHang { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string SoLuong { get; set; }
        public string DonGia { get; set; }
        public double ThanhTien { get; set; }
    }
}
