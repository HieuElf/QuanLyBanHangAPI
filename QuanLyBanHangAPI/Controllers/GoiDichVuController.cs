using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.GoiDichVu;
using QuanLyBanHangAPI.Services.GoiDIchVuServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System.Data;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoiDichVuController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;
        private readonly IGoiDichVuServices _goiDichVuServices;
        public GoiDichVuController(ITokenServices tokenServices, IGoiDichVuServices goiDichVuServices)
        {
            _tokenServices = tokenServices;
            _goiDichVuServices = goiDichVuServices;
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
            try
            {
                return Ok(_goiDichVuServices.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var goi = _goiDichVuServices.GetById(id);
                if (goi != null)
                {
                    return Ok(goi);
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
        public IActionResult Add(GoiDichVuModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (model.TenGoi == null)
                    {
                        return BadRequest("Tên gói chưa điền");
                    }
                    var checktengoi = _goiDichVuServices.GetByName(model.TenGoi);
                    if (checktengoi != null)
                    {
                        return BadRequest("Tên gói đã tồn tại");
                    }
                    return Ok(_goiDichVuServices.Add(model));
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
        public IActionResult Update(GoiDichVuVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                if (vm.TenGoi == null)
                {
                    return BadRequest("Tên gói chưa điền");
                }
                string result = _goiDichVuServices.Update(vm);
                switch (result)
                {
                    case "OK":
                        return Ok("Cập nhật thành công");
                    case "Đã tồn tại dữ liệu khác trùng tên":
                        return BadRequest(result);
                    case "Không tồn tại":
                        return NotFound(result);
                }
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
                var goi = _goiDichVuServices.GetById(id);
                if (goi == null)
                {
                    return NotFound();
                }
                _goiDichVuServices.Delete(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }

    }
}
