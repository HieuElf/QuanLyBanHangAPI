using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHangAPI.Models.NhaCungCap
{
    public class NhaCungCapModel
    {
        [Required]
        [MaxLength(255)]
        public string TenNhaCungCap { get; set; }
        public string TrangChu { get; set; }
    }
}
