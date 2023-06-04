using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Data.DTO;
using QuanLyBanHangAPI.Models.Blog;
using QuanLyBanHangAPI.Models.ChuyenMucBlog;
using QuanLyBanHangAPI.Services.BlogServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;
        private readonly IBlogServices _blogServices;
        private readonly IWebHostEnvironment _environment;
        public BlogController(ITokenServices tokenServices, IBlogServices blogServices,IWebHostEnvironment environment)
        {
            _tokenServices = tokenServices;
            _blogServices = blogServices;
            _environment = environment;
        }
        private string GetJwtToken()
        {
            // Lấy JWT bearer token từ header của HttpRequest
            string jwtBearerToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return jwtBearerToken;
        }
        private static bool IsImage(string fileName)
        {
            string[] acceptedExtensions = { ".jpg", ".jpeg", ".png", ".gif"};
            string extension = Path.GetExtension(fileName);
            return acceptedExtensions.Contains(extension.ToLower());
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
                return Ok(_blogServices.GetAll());
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
                var baiviet = _blogServices.GetByID(id);
                if (baiviet != null)
                {
                    return Ok(baiviet);
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
        public async Task<IActionResult> Add([FromForm]BlogModelDto model)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    var baiviet = new BlogModel();
                    if (model.tenBaiViet == null)
                    {
                        return BadRequest("Tên bài viết không được để trông");
                    }
                    baiviet.tenBaiViet = model.tenBaiViet;
                    baiviet.maChuyenMuc = model.maChuyenMuc;
                    baiviet.tomTat = model.tomTat;
                    baiviet.ngayBaiViet = DateTime.Now;
                    baiviet.ngayChinhSuaCuoi = DateTime.Now;
                    baiviet.noiDung = model.noiDung;
                    baiviet.trangThai = model.trangThai;
                    string ngay = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +DateTime.Now.Year.ToString();
                    if (model.anhBia != null || model.anhBia.Length != 0)
                    {
                        bool checkAnhBia = IsImage(model.anhBia.FileName);
                        if (checkAnhBia)
                        {
                            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "blogs", "anhbia", ngay,model.tenBaiViet);
                            if (!Directory.Exists(uploadsFolderPath))
                            {
                                Directory.CreateDirectory(uploadsFolderPath);
                            }
                            var filePath = Path.Combine(uploadsFolderPath, model.anhBia.FileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await model.anhBia.CopyToAsync(stream);
                                stream.Flush();
                                baiviet.anhBia = model.anhBia.FileName;
                            }
                        }
                        else
                        {
                            return BadRequest("Chỉ chấp nhận file ảnh có định dạng (.jpg, .jpeg, .png, .gif)");
                        }
                    }
                    if (model.anhBaiViet != null && model.anhBaiViet.Count > 0)
                    {
                        foreach (var anh in model.anhBaiViet)
                        {
                            if (anh.Length != 0)
                            {
                                bool checkAnhBaiViet = IsImage(anh.FileName);
                                if (checkAnhBaiViet)
                                {
                                    var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "blogs", "anhbaiviet", ngay,model.tenBaiViet);
                                    if (!Directory.Exists(uploadsFolderPath))
                                    {
                                        Directory.CreateDirectory(uploadsFolderPath);
                                    }
                                    var filePath = Path.Combine(uploadsFolderPath, anh.FileName);
                                    using (var stream = new FileStream(filePath, FileMode.Create))
                                    {
                                        await anh.CopyToAsync(stream);
                                        stream.Flush();
                                    }
                                    baiviet.anhBaiViet += anh.FileName +":";
                                }
                                else
                                {
                                    return BadRequest("Một số file upload không đúng định dạng.Chỉ chấp nhận file ảnh có định dạng (.jpg, .jpeg, .png, .gif)");
                                }
                            }
                        }
                    }
                    return Ok(_blogServices.Add(baiviet));
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
        public async Task<IActionResult> Update([FromForm]int id,[FromForm]BlogModelDto blogModelDto)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (blogModelDto.tenBaiViet == null)
                    {
                        return BadRequest("Tên bài viết không được để trống");
                    }
                    var baiVietInDB = _blogServices.GetByID(id);
                    if (baiVietInDB == null)
                    {
                        return BadRequest("Không tồn tại");
                    }
                    var baiviet = new BlogVM();

                    baiviet.maBaiViet = id;
                    baiviet.tenBaiViet = blogModelDto.tenBaiViet;
                    baiviet.maChuyenMuc = blogModelDto.maChuyenMuc;
                    baiviet.tomTat = blogModelDto.tomTat;
                    baiviet.noiDung = blogModelDto.noiDung;
                    baiviet.trangThai = blogModelDto.trangThai;
                    baiviet.ngayChinhSuaCuoi = DateTime.Now;
                    baiviet.ngayBaiViet = baiVietInDB.ngayBaiViet;
                    
                    string ngay = baiVietInDB.ngayBaiViet.Day.ToString() + "-" + baiVietInDB.ngayBaiViet.Month.ToString() + "-" + baiVietInDB.ngayBaiViet.Year.ToString();
                    if (blogModelDto.anhBia != null && blogModelDto.anhBia.Length != 0)
                    {
                        bool checkAnhBia = IsImage(blogModelDto.anhBia.FileName);
                        if (checkAnhBia)
                        {
                            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "blogs", "anhbia", ngay, blogModelDto.tenBaiViet);
                            if (!Directory.Exists(uploadsFolderPath))
                            {
                                Directory.CreateDirectory(uploadsFolderPath);
                            }
                            var filePath = Path.Combine(uploadsFolderPath, blogModelDto.anhBia.FileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await blogModelDto.anhBia.CopyToAsync(stream);
                                stream.Flush();
                                baiviet.anhBia = blogModelDto.anhBia.FileName;
                            }
                        }
                        else
                        {
                            return BadRequest("Chỉ chấp nhận file ảnh có định dạng (.jpg, .jpeg, .png, .gif)");
                        }
                    }
                    else
                    {
                        baiviet.anhBia = baiVietInDB.anhBia;
                    }

                    if (blogModelDto.anhBaiViet != null && blogModelDto.anhBaiViet.Count > 0)
                    {
                        foreach (var anh in blogModelDto.anhBaiViet)
                        {
                            if (anh.Length != 0)
                            {
                                bool checkAnhBaiViet = IsImage(anh.FileName);
                                if (checkAnhBaiViet)
                                {
                                    var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "blogs", "anhbaiviet", ngay, blogModelDto.tenBaiViet);
                                    if (!Directory.Exists(uploadsFolderPath))
                                    {
                                        Directory.CreateDirectory(uploadsFolderPath);
                                    }
                                    var filePath = Path.Combine(uploadsFolderPath, anh.FileName);
                                    using (var stream = new FileStream(filePath, FileMode.Create))
                                    {
                                        await anh.CopyToAsync(stream);
                                        stream.Flush();
                                    }
                                    baiviet.anhBaiViet += anh.FileName + ":";
                                }
                                else
                                {
                                    return BadRequest("Một số file upload không đúng định dạng.Chỉ chấp nhận file ảnh có định dạng (.jpg, .jpeg, .png, .gif)");
                                }
                            }
                        }
                    }
                    else
                    {
                        baiviet.anhBaiViet = baiVietInDB.anhBaiViet;
                    }

                    string result = _blogServices.Update(baiviet);
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
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
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
                var ncc = _blogServices.GetByID(id);
                if (ncc == null)
                {
                    return NotFound();
                }
                _blogServices.Delete(id);
                return Ok("Xóa thành công");
            }
            return BadRequest("Token đã hết hạn");
        }
    }
}
