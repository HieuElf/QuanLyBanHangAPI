using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("GoiDichVu")]
    [Index(nameof(GoiDichVu.TenGoi), IsUnique = true)]

    public class GoiDichVu
    {
        [Key]
        public int MaGoi { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenGoi { get; set; }
        public string MoTa { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
