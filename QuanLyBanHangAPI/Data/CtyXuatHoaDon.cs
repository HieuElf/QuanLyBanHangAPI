using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("CtyXuatHoaDon")]
    public class CtyXuatHoaDon
    {
        [Key]
        public int MaCty { get; set; }
        [Required]
        public string TenCty { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
    }
}
