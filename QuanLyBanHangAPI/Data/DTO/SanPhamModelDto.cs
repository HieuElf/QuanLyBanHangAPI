using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Data.DTO
{
    public class SanPhamModelDto
    {
        public string TenSP { get; set; }
        public int? MaGoi { get; set; }
        public int? MaNhaCungCap { get; set; }
        public string TomTat { get; set; }
        public string MoTa { get; set; }
        public IFormFile AnhSP { get; set; }
        public List<IFormFile> ListAnh { get; set; }
        public string NoiDung { get; set; }
        public double Gia { get; set; }
        public double GiamGia { get; set; }
        public bool TrangThai { get; set; }
    }
}
