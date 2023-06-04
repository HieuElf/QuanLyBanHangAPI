using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Data.DTO;
using QuanLyBanHangAPI.Services.DonDatHangServices;
using QuanLyBanHangAPI.Services.TokenServices;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateDonHangController : ControllerBase
    {
        private readonly IDonDatHangServices _donDatHangServices;
        private readonly ITokenServices _tokenServices;
        public UpdateDonHangController(IDonDatHangServices donDatHangServices, ITokenServices tokenServices)
        {
            _donDatHangServices = donDatHangServices;
            _tokenServices = tokenServices;
        }
        private string GetJwtToken()
        {
            // Lấy JWT bearer token từ header của HttpRequest
            string jwtBearerToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return jwtBearerToken;
        }
        private bool CheckIsTokenExpired()
        {
            string token = GetJwtToken();
            bool check = _tokenServices.IsTokenExpired(token);
            return check;
        }
        [HttpPost]
        public IActionResult UpdateDonHang(UpdateDonHangDto dto)
        {
            bool checkToken = CheckIsTokenExpired();
            if (!checkToken)
            {
                try
                {
                    var donhang = _donDatHangServices.GetByID(dto.MaDonHang);
                    if (donhang == null)
                    {
                        return NotFound();
                    }
                    bool result = _donDatHangServices.UpdateDonHang(dto);
                    if (result)
                    {
                        return Ok();
                    }
                    return BadRequest("Có lỗi trong quá trình cập nhật");
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
