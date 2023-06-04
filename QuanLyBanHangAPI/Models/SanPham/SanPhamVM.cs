using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Models.SanPham
{
    public class SanPhamVM
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int? MaGoi { get; set; }
        public int? MaNhaCungCap { get; set; }
        public string TomTat { get; set; }
        public string MoTa { get; set; }
        public string AnhSP { get; set; }
        public string ListAnh { get; set; }
        public string NoiDung { get; set; }
        public int LuotXem { get; set; }
        public int DaBan { get; set; }
        public double Gia { get; set; }
        public double GiamGia { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayChinhSuaCuoi { get; set; }
        public bool TrangThai { get; set; }
    }
}
