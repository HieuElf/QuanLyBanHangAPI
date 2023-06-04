using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.DonDatHang;
using QuanLyBanHangAPI.Services.DonDatHangServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System;
using System.Data;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonDatHangController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;
        private readonly IDonDatHangServices _donDatHangServices;
        public DonDatHangController(ITokenServices tokenServices, IDonDatHangServices donDatHangServices)
        {
            _tokenServices = tokenServices;
            _donDatHangServices = donDatHangServices;
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
                return Ok(_donDatHangServices.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var donhang = _donDatHangServices.GetByID(id);
                if (donhang != null)
                {
                    return Ok(donhang);
                }
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult Add(DonDatHangModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (model.TenKhachHang == null || 
                        model.DiaChi == null ||
                        model.Email == null ||
                        model.SoDienThoai == null)
                    {
                        return BadRequest("Vui lòng điền đủ thông tin đã đánh dấu *");
                    }
                    return Ok(_donDatHangServices.Add(model));
                }
                catch
                {

                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest("Token đã hết hạn");
        }
        [HttpPut("{id}")]
        public IActionResult Update(DonDatHangVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                if (vm.TenKhachHang == null ||
                    vm.DiaChi == null ||
                    vm.Email == null ||
                    vm.SoDienThoai == null)
                {
                    return BadRequest("Vui lòng điền đủ thông tin đã đánh dấu *");
                }
                bool result = _donDatHangServices.Update(vm);
                if (!result)
                {
                    return BadRequest("Cập nhật thất bại");
                }
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Token đã hết hạn");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                var donhang = _donDatHangServices.GetByID(id);
                if (donhang == null)
                {
                    return NotFound();
                }
                _donDatHangServices.Delete(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
