using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.SanPham;
using QuanLyBanHangAPI.Services.SanPhamServices;
using QuanLyBanHangAPI.Services.TokenServices;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamServices _sanPhamServices;
        private readonly ITokenServices _tokenServices;
        public SanPhamController(ISanPhamServices sanPhamServices, ITokenServices tokenServices)
        {
            _sanPhamServices = sanPhamServices;
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
                    return Ok(_sanPhamServices.GetAll());
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
                    var ncc = _sanPhamServices.GetByID(id);
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
        public IActionResult Add(SanPhamModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (model.TenSP == "" || model.TenSP == null)
                    {
                        return BadRequest("Chưa điền tên sản phẩm");
                    }
                    var sp = _sanPhamServices.GetByName(model.TenSP);
                    if (sp != null)
                    {
                        return BadRequest("Tên sản phẩm đã tồn tại");
                    }
                    return Ok(_sanPhamServices.Add(model));
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
        public IActionResult Update(SanPhamVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var ncc = _sanPhamServices.GetByID(vm.MaSP);
                if (ncc != null)
                {
                    if (vm.TenSP == null)
                    {
                        return BadRequest("Chưa điền tên sản phẩm");
                    }
                    var sp = _sanPhamServices.GetByName(vm.TenSP);
                    if (sp != null)
                    {
                        return BadRequest("Tên sản phẩm đã tồn tại");
                    }
                    _sanPhamServices.Update(vm);
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
                var ncc = _sanPhamServices.GetByID(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _sanPhamServices.Delete(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
