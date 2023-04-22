using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHangAPI.Models.TaiKhoanNhanThanhToan
{
    public class TaiKhoanNhanThanhToanModel
    {
        [Required, MaxLength(50)]
        public string TenTKNhan { get; set; }
        [Required]
        public string STKNhan { get; set; }
        [Required]
        public string NganHang { get; set; }
        public string ChiNhanh { get; set; }
    }
}
