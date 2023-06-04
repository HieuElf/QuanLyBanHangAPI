using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("DonViChuyenPhat")]
    [Index(nameof(DonViChuyenPhat.TenDonVi), IsUnique = true)]
    public class DonViChuyenPhat
    {
        [Key]
        public int MaDonViChuyenPhat { get; set; }
        [Required, MaxLength(200)]
        public string TenDonVi { get; set; }
        [MaxLength(12)]
        public string SDT { get; set; }
        public string GhiChu { get; set; }
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
