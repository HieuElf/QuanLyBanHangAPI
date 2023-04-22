using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Models.SanPham
{
    public class SanPhamVM
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int? MaGoi { get; set; }
        public string TomTat { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public bool TrangThai { get; set; }
    }
}
