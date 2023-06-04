using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("SanPham")]
    [Index(nameof(SanPham.TenSP), IsUnique = true)]

    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }
        [Required]
        public string TenSP { get; set; }
        public int? MaGoi { get; set; }
        [ForeignKey("MaGoi")]
        public GoiDichVu GoiDichVu { get; set; }
        public int? MaNhaCungCap { get; set; }
        [ForeignKey("MaNhaCungCap")]
        public NhaCungCap NhaCungCap { get; set; }
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
