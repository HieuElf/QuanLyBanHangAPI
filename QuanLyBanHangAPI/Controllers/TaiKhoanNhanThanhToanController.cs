using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.TaiKhoanNhanThanhToan;
using QuanLyBanHangAPI.Services.TaiKhoanNhanThanhToanServices;
using QuanLyBanHangAPI.Services.TokenServices;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaiKhoanNhanThanhToanController : ControllerBase
    {
        private readonly ITaiKhoanNhanThanhToanServices _taiKhoanNhanThanhToanServices;
        private readonly ITokenServices _tokenServices;
        public TaiKhoanNhanThanhToanController(ITaiKhoanNhanThanhToanServices taiKhoanNhanThanhToanServices, ITokenServices tokenServices)
        {
            _taiKhoanNhanThanhToanServices = taiKhoanNhanThanhToanServices;
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
                    return Ok(_taiKhoanNhanThanhToanServices.GetAll());
                }
                catch
                {
                    StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest("Token đã hết hạn");
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            bool check = CheckIsTokenExpired();
            if (check)
            {
                return BadRequest("Token đã hết hạn");
            }
            try
            {
                var ncc = _taiKhoanNhanThanhToanServices.GetById(id);
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
        [HttpPost]
        [Authorize(Roles = "Ad")]
        public IActionResult Add(TaiKhoanNhanThanhToanModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    var stk = _taiKhoanNhanThanhToanServices.GetByName(model.STKNhan);
                    if (stk != null)
                    {
                        return BadRequest("Số tài khoản đã tồn tại");
                    }
                    return Ok(_taiKhoanNhanThanhToanServices.Add(model));
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
        public IActionResult Update(int id,TaiKhoanNhanThanhToanVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var ncc = _taiKhoanNhanThanhToanServices.GetById(id);
                if (ncc != null)
                {
                    var stk = _taiKhoanNhanThanhToanServices.GetByName(vm.STKNhan);
                    if (stk != null)
                    {
                        return BadRequest("Số tài khoản đã tồn tại");
                    }
                    _taiKhoanNhanThanhToanServices.Update(vm);
                    return Ok("Cập nhật thành công");
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
                var ncc = _taiKhoanNhanThanhToanServices.GetById(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _taiKhoanNhanThanhToanServices.Delete(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
