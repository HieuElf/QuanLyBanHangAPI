using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.NhaCungCap;
using QuanLyBanHangAPI.Services.NhaCungCapServices;
using QuanLyBanHangAPI.Services.TokenServices;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController : ControllerBase
    {
        private readonly INhaCungCapServices _nhaCungCapServices;
        private readonly ITokenServices _tokenServices;
        public NhaCungCapController(INhaCungCapServices nhaCungCapServices,ITokenServices tokenServices)
        {
            _nhaCungCapServices = nhaCungCapServices;
            _tokenServices = tokenServices;
        }
        [HttpGet]
        [Authorize(Roles ="Ad")]
        public IActionResult GetAll() 
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return StatusCode(StatusCodes.Status401Unauthorized);
            //}
            //if (!User.IsInRole("Ad"))
            //{
            //    return StatusCode(StatusCodes.Status403Forbidden);
            //}

            // Lấy JWT bearer token từ header của HttpRequest
            var jwtBearerToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            // Checking IsTokenExpired?
            bool check = _tokenServices.IsTokenExpired(jwtBearerToken);
            if (check == false)
            {
                try
                {
                    return Ok(_nhaCungCapServices.GetAll());
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return StatusCode(StatusCodes.Status419AuthenticationTimeout);
        }
        [HttpPost]
        public IActionResult Add(NhaCungCapModel model)
        {
            try
            {
                return Ok(_nhaCungCapServices.Add(model));
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
