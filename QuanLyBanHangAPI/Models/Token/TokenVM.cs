using System.ComponentModel.DataAnnotations;
using System;

namespace QuanLyBanHangAPI.Models.Token
{
    public class TokenVM
    {
        public string TokenKey { get; set; }
        public bool TokenIsReVoked { get; set; }
        public bool IsRevoked { get; set; }
    }
}
