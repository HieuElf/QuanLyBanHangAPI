using System;

namespace QuanLyBanHangAPI.Models.DonDatHang
{
    public class DonDatHangModel
    {
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        //public bool DaThanhToan { get; set; }
        public int MaDonViChuyenPhat { get; set; }
        public string TinhTrangGiaoHang { get; set; }
        public string MaGiaoDichVNPay { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string TrangThaiDonHang { get; set; }
    }
}
