namespace QuanLyBanHangAPI.Models.KhachHangOder
{
    public class KhachHangOderVM
    {
        public int Id { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTy { get; set; }
        public string TenNguoiLienHe { get; set; }
        public string SoDienThoai { get; set; }
        public int? MaNhaCungCap { get; set; }
        public int? MaGoi { get; set; }
    }
}
