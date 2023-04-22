using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("HopDongChuKySo")]
    public class HopDongChuKySo
    {
        public int MaHopDong { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTy { get; set; }
        public string DiaChi { get; set; }
        public int MaGoi { get; set; }
        public double ThucThu { get; set; }
    }
}
