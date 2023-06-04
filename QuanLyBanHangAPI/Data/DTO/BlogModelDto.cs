using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Data.DTO
{
    public class BlogModelDto
    {
        public int maChuyenMuc { get; set; }
        public string tenBaiViet { get; set; }
        public string tomTat { get; set; }
        public IFormFile anhBia { get; set; }
        public string noiDung { get; set; }
        public List<IFormFile> anhBaiViet { get; set; }
        public bool trangThai { get; set; }
    }
}
