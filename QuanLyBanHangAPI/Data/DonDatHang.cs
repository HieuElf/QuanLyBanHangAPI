using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace QuanLyBanHangAPI.Data
{
    [Table("DonDatHang")]
    public class DonDatHang
    {
        [Key]
        public Guid MaDonHang { get; set; }
        [Required]
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        //public bool DaThanhToan { get; set; }
        [AllowNull]
        public DateTime NgayDatHang { get; set; }
        public int MaDonViChuyenPhat { get; set; }
        [ForeignKey("MaDonViChuyenPhat")]
        public DonViChuyenPhat DonViChuyenPhat { get; set; }
        public string TinhTrangGiaoHang { get; set; }
        public DateTime NgayGiao { get; set; }
        public string MaGiaoDichVNPay { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string TrangThaiDonHang { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

    }
}
