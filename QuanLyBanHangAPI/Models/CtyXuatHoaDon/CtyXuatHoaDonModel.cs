using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHangAPI.Models.CtyXuatHoaDon
{
    public class CtyXuatHoaDonModel
    {
        [Required]
        public string TenCty { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
    }
}
