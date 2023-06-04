using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("Blog")]
    [Index(nameof(Blog.tenBaiViet), IsUnique = true)]
    public class Blog
    {
        [Key]
        public int maBaiViet { get; set; }
        [Required]
        public int? maChuyenMuc { get; set; }
        [ForeignKey("maChuyenMuc")]
        public ChuyenMucBlog ChuyenMucBlog { get; set; }
        [Required]
        public string tenBaiViet { get; set; }
        [MaxLength(500)]
        public string tomTat { get; set; }
        [MaxLength(200)]
        public string anhBia { get; set; }
        public string noiDung { get; set; }
        public string anhBaiViet { get; set; }
        public DateTime ngayBaiViet { get; set; }
        public DateTime ngayChinhSuaCuoi { get; set; }
        public int luotXem { get; set; }
        public bool trangThai { get; set; }
    }
}
