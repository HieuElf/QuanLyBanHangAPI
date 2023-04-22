using System.ComponentModel.DataAnnotations;
using System;

namespace QuanLyBanHangAPI.Models.Token
{
    public class TokenModel
    {
        public string UserId { get; set; }
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
