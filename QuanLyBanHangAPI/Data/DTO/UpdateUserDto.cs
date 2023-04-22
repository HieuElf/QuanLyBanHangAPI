using Microsoft.AspNetCore.Http;

namespace QuanLyBanHangAPI.Data.DTO
{
    public class UpdateUserDto
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public IFormFile avatarUrl { get; set; }
    }
}
