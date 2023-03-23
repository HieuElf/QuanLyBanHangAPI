using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHangAPI.Data
{
    [Table("Token")]
    public class Token
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string TokenKey { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime VaiLidTo { get; set; }
        public bool TokenIsUsed { get; set; }
        public bool TokenIsReVoked { get; set; }
        public string ReFreshToken { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
    }
}
