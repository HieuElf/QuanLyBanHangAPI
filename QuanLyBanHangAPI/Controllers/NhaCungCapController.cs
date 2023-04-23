using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.NhaCungCap;
using QuanLyBanHangAPI.Services.NhaCungCapServices;
using QuanLyBanHangAPI.Services.TokenServices;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NhaCungCapController : ControllerBase
    {
        private readonly INhaCungCapServices _nhaCungCapServices;
        private readonly ITokenServices _tokenServices;
        public NhaCungCapController(INhaCungCapServices nhaCungCapServices, ITokenServices tokenServices)
        {
            _nhaCungCapServices = nhaCungCapServices;
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
                    return Ok(_nhaCungCapServices.GetAll());
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
                    var ncc = _nhaCungCapServices.GetById(id);
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
        public IActionResult Add(NhaCungCapModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (model.TenNhaCungCap == null)
                    {
                        return BadRequest("Chưa điền tên nhà cung cấp");
                    }
                    var tenncc = _nhaCungCapServices.GetByName(model.TenNhaCungCap);
                    if (tenncc != null)
                    {
                        return BadRequest("Tên nhà cung cấp đã tồn tại");
                    }
                    return Ok(_nhaCungCapServices.Add(model));
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
        public IActionResult Update(NhaCungCapVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var ncc = _nhaCungCapServices.GetById(vm.MaNhaCungCap);
                if (ncc != null)
                {
                    if (vm.TenNhaCungCap == null)
                    {
                        return BadRequest("Chưa điền tên nhà cung cấp");
                    }
                    var tenncc = _nhaCungCapServices.GetByName(vm.TenNhaCungCap);
                    if (tenncc != null)
                    {
                        return BadRequest("Tên nhà cung cấp đã tồn tại");
                    }
                    _nhaCungCapServices.Update(vm);
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
                var ncc = _nhaCungCapServices.GetById(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _nhaCungCapServices.DeleteById(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}