using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("TaiKhoanNhanThanhToan")]
    public class TaiKhoanNhanThanhToan
    {
        [Key]
        public int IdTK { get; set; }
        [Required, MaxLength(50)]
        public string TenTKNhan { get; set; }
        [Required]
        public string STKNhan { get; set; }
        [Required]
        public string NganHang { get; set; }
        public string ChiNhanh { get; set; }
    }
}
