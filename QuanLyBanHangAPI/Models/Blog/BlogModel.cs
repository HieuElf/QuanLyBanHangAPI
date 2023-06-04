using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Models.Blog
{
    public class BlogModel
    {
        public int maChuyenMuc { get; set; }
        public string tenBaiViet { get; set; }
        public string tomTat { get; set; }
        public string anhBia { get; set; }
        public string noiDung { get; set; }
        public string anhBaiViet { get; set; }
        public DateTime ngayBaiViet { get; set; }
        public DateTime ngayChinhSuaCuoi { get; set; }
        public bool trangThai { get; set; }
    }
}
