using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.KhachHangOder;
using QuanLyBanHangAPI.Services.KhachHangOderServices;
using QuanLyBanHangAPI.Services.TokenServices;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangOderController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;
        private readonly IKhachHangOderServices _khachHangOderServices;
        public KhachHangOderController(ITokenServices tokenServices, IKhachHangOderServices khachHangOderServices)
        {
            _tokenServices = tokenServices;
            _khachHangOderServices = khachHangOderServices;
        }
        private string GetJwtToken()
        {
            // Lấy JWT bearer token từ header của HttpRequest
            string jwtBearerToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return jwtBearerToken;
        }
        private bool CheckIsTokenExpired()
        {
            string tokencheck = GetJwtToken();
            bool check = _tokenServices.IsTokenExpired(tokencheck);
            return check;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    return Ok(_khachHangOderServices.GetAll());
                }
                catch
                {
                    StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest("Token đã hết hạn");
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            bool check = CheckIsTokenExpired();
            if (!check)
            {
                try
                {
                    var ncc = _khachHangOderServices.GetById(id);
                    if (ncc != null)
                    {
                        return Ok(ncc);
                    }
                    return NotFound();
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest("Token đã hết hạn");
        }
        [HttpPost]
        public IActionResult Add(KhachHangOderModel model)
        {
            try
            {
                return Ok(_khachHangOderServices.Add(model));
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Ad")]
        public IActionResult Update(int id, KhachHangOderVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var ncc = _khachHangOderServices.GetById(id);
                if (ncc != null)
                {
                    _khachHangOderServices.Update(vm);
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest("Token đã hết hạn");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Ad")]
        public IActionResult Delete(int id)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var ncc = _khachHangOderServices.GetById(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _khachHangOderServices.Delete(id);
                return Ok();
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
