using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("NhaCungCap")]
    [Index(nameof(NhaCungCap.TenNhaCungCap), IsUnique = true)]
    public class NhaCungCap
    {
        [Key]
        public int MaNhaCungCap { get; set; }
        [Required]
        public string TenNhaCungCap { get; set; }
        public string TrangChu { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
