
using Microsoft.AspNetCore.Http;

namespace QuanLyBanHangAPI.Models.SanPham
{
    public class SanPhamModel
    {
        public string TenSP { get; set; }
        public int? MaGoi { get; set; }
        public string TomTat { get; set; }
        public string MoTa { get; set; }
        public IFormFile HinhAnhUri { get; set; }
        public bool TrangThai { get; set; }
    }
}
