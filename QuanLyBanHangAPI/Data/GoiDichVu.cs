using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("GoiDichVu")]
    public class GoiDichVu
    {
        [Key]
        public int MaGoi { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenGoi { get; set; }
        [Required]
        public int? MaNhaCungCap { get; set; }
        [ForeignKey("MaNhaCungCap")]
        public NhaCungCap NhaCungCap { get; set; }
        public virtual ICollection<KhachHangOder> KhachHangOders { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
