using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.CtyXuatHoaDon;
using QuanLyBanHangAPI.Services.CtyXuatHoaDonServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System.Data;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CtyXuatHoaDonController : ControllerBase
    {
        private readonly ICtyXuatHoaDonServices _ctyXuatHoaDonServices;
        private readonly ITokenServices _tokenServices;
        public CtyXuatHoaDonController(ICtyXuatHoaDonServices ctyXuatHoaDonServices, ITokenServices tokenServices)
        {
            _ctyXuatHoaDonServices = ctyXuatHoaDonServices;
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
        [HttpGet]
        public IActionResult GetAll()
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    return Ok(_ctyXuatHoaDonServices.GetAll());
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
                    var ncc = _ctyXuatHoaDonServices.GetById(id);
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
        public IActionResult Add(CtyXuatHoaDonModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (model.TenCty == null)
                    {
                        return BadRequest("Tên công ty không được để trống");
                    }
                    var cty = _ctyXuatHoaDonServices.GetByName(model.TenCty);
                    if (cty != null)
                    {
                        return BadRequest("Tên công ty đã tồn tại");
                    }
                    
                    return Ok(_ctyXuatHoaDonServices.Add(model));
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
        public IActionResult Update(int id, CtyXuatHoaDonVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var ncc = _ctyXuatHoaDonServices.GetById(id);
                if (ncc != null)
                {
                    if (vm.TenCty == null)
                    {
                        return BadRequest("Tên công ty không được để trống");
                    }
                    var cty = _ctyXuatHoaDonServices.GetByName(vm.TenCty);
                    if (cty != null)
                    {
                        return BadRequest("Tên công ty đã tồn tại");
                    }
                    _ctyXuatHoaDonServices.Update(vm);
                    return Ok("Update thành công");
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
                var ncc = _ctyXuatHoaDonServices.GetById(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _ctyXuatHoaDonServices.DeleteById(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
