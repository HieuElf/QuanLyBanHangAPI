using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Models.ChuyenMucBlog;
using QuanLyBanHangAPI.Models.DonViChuyenPhat;
using QuanLyBanHangAPI.Services.ChuyenMucBlogServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System.Data;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuyenMucBlogController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;
        private readonly IChuyenMucBlogServices _chuyenMucBlogServices;
        public ChuyenMucBlogController(ITokenServices tokenServices, IChuyenMucBlogServices chuyenMucBlogServices)
        {
            _tokenServices = tokenServices;
            _chuyenMucBlogServices = chuyenMucBlogServices;
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
            try
            {
                return Ok(_chuyenMucBlogServices.GetAll());
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
                var cm = _chuyenMucBlogServices.ChuyenMucBlogVMGetById(id);
                if (cm != null)
                {
                    return Ok(cm);
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
        public IActionResult Add(ChuyenMucBlogModel model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (model.tenChuyenMuc == null)
                    {
                        return BadRequest("Tên chuyên mục không được để trống");
                    }
                    var checkchuyenmuc = _chuyenMucBlogServices.ChuyenMucBlogVMGetByName(model.tenChuyenMuc);
                    if (checkchuyenmuc != null)
                    {
                        return BadRequest("Tên chuyên mục đã tồn tại");
                    }
                    return Ok(_chuyenMucBlogServices.Add(model));
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
        public IActionResult Update(ChuyenMucBlogVM vm)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                if (vm.tenChuyenMuc == null)
                {
                    return BadRequest("Tên chuyên mục không được để trống");
                }

                string result = _chuyenMucBlogServices.Update(vm);
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
                var ncc = _chuyenMucBlogServices.ChuyenMucBlogVMGetById(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _chuyenMucBlogServices.Delete(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
