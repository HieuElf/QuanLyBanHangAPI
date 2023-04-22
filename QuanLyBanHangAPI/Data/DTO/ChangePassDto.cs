using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHangAPI.Data.DTO
{
    public class ChangePassDto
    {
        public string userName { get; set; }
        public string currentPass { get; set; }
        public string newPass { get; set; }
        public string comfirmPass { get; set; }
    }
}
