using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHangAPI.Models.DonViChuyenPhat
{
    public class DonViChuyenPhatModel
    {
        [Required, MaxLength(200)]
        public string TenDonVi { get; set; }
        [MaxLength(12)]
        public string SDT { get; set; }
        public string GhiChu { get; set; }
    }
}
