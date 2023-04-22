using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.DonViChuyenPhat;
using QuanLyBanHangAPI.Services.DonViChuyenPhatServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System.Data;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonViChuyenPhatController : ControllerBase
    {
        private readonly IDonViChuyenPhatServices _donViChuyenPhatServices;
        private readonly ITokenServices _tokenServices;
        public DonViChuyenPhatController(IDonViChuyenPhatServices donViChuyenPhatServices, ITokenServices tokenServices)
        {
            _donViChuyenPhatServices = donViChuyenPhatServices;
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
            string tokencheck = GetJwtToken();
            bool check = _tokenServices.IsTokenExpired(tokencheck);
            return check;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    return Ok(_donViChuyenPhatServices.GetAll());
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
                    var ncc = _donViChuyenPhatServices.GetByID(id);
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
        [Authorize(Roles = "Ad")]
        public IActionResult Add(DonViChuyenPhatModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    return Ok(_donViChuyenPhatServices.Add(model));
                }
                catch
                {

                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest("Token đã hết hạn");
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Ad")]
        public IActionResult Update(int id, DonViChuyenPhatVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var ncc = _donViChuyenPhatServices.GetByID(id);
                if (ncc != null)
                {
                    _donViChuyenPhatServices.Update(vm);
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
                var ncc = _donViChuyenPhatServices.GetByID(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _donViChuyenPhatServices.Delete(id);
                return Ok();
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
