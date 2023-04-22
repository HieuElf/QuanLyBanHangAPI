using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }
        [Required]
        public string TenSP { get; set; }
        public int? MaGoi { get; set; }
        [ForeignKey("MaGoi")]
        public GoiDichVu GoiDichVu { get; set; }
        public string TomTat { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public bool TrangThai { get; set; }
    }
}
