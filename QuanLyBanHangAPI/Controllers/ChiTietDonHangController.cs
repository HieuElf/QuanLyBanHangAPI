using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.ChiTietDonHang;
using QuanLyBanHangAPI.Models.DonDatHang;
using QuanLyBanHangAPI.Services.ChiTietDonHangServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietDonHangController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;
        private readonly IChiTietDonHangServices _chiTietDonHangServices;
        public ChiTietDonHangController(ITokenServices tokenServices, IChiTietDonHangServices chiTietDonHangServices)
        {
            _tokenServices = tokenServices;
            _chiTietDonHangServices = chiTietDonHangServices;
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
                return Ok(_chiTietDonHangServices.GetAll());
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
                var donhang = _chiTietDonHangServices.GetById(id);
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
        public IActionResult Add(ChiTietDonHangModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (model.TenSP == null ||
                        model.MaSP == null ||
                        model.SoLuong == null)
                    {
                        return BadRequest("Vui lòng điền đủ thông tin đã đánh dấu *");
                    }
                    return Ok(_chiTietDonHangServices.Add(model));
                }
                catch
                {

                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest("Token đã hết hạn");
        }
        [HttpPut("{id}")]
        public IActionResult Update(ChiTietDonHangVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                if (vm.MaSP == null ||
                    vm.TenSP == null ||
                    vm.SoLuong == null ||
                    vm.DonGia == null)
                {
                    return BadRequest("Vui lòng điền đủ thông tin đã đánh dấu *");
                }
                bool result = _chiTietDonHangServices.Update(vm);
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
                var donhang = _chiTietDonHangServices.GetById(id);
                if (donhang == null)
                {
                    return NotFound();
                }
                _chiTietDonHangServices.Delete(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
