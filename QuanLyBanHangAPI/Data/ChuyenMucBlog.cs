using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("ChuyenMucBlog")]
    [Index(nameof(ChuyenMucBlog.tenChuyenMuc), IsUnique = true)]
    public class ChuyenMucBlog
    {
        [Key]
        public int maChuyenMuc { get; set; }
        public string tenChuyenMuc { get; set; }
        public string moTa { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
