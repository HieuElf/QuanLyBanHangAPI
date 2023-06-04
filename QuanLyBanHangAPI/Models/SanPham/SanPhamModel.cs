
using Microsoft.AspNetCore.Http;
using System;

namespace QuanLyBanHangAPI.Models.SanPham
{
    public class SanPhamModel
    {
        public string TenSP { get; set; }
        public int? MaGoi { get; set; }
        public int? MaNhaCungCap { get; set; }
        public string TomTat { get; set; }
        public string MoTa { get; set; }
        public string AnhSP { get; set; }
        public string ListAnh { get; set; }
        public string NoiDung { get; set; }
        public double Gia { get; set; }
        public double GiamGia { get; set; }
        public bool TrangThai { get; set; }
    }
}
