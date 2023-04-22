using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("DonViChuyenPhat")]
    public class DonViChuyenPhat
    {
        [Key]
        public int MaDonVi { get; set; }
        [Required, MaxLength(200)]
        public string TenDonVi { get; set; }
        [MaxLength(12)]
        public string SDT { get; set; }
        public string GhiChu { get; set; }
    }
}
