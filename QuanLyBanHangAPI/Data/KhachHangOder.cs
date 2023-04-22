using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("KhachHangOder")]
    public class KhachHangOder
    {
        [Key, Required]
        public int Id { get; set; }
        [Required, MaxLength(15)]
        public string MaSoThue { get; set; }
        [Required]
        public string TenCongTy { get; set; }
        [Required]
        public string TenNguoiLienHe { get; set; }
        [Required, MaxLength(11)]
        public string SoDienThoai { get; set; }
        public int? MaNhaCungCap { get; set; }
        [ForeignKey("MaNhaCungCap")]
        public NhaCungCap NhaCungCap { get; set; }
        public int? MaGoi { get; set; }
        [ForeignKey("MaGoi")]
        public GoiDichVu GoiDichVu { get; set; }
    }
}
