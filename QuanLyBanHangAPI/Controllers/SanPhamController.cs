using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangAPI.Data.DTO;
using QuanLyBanHangAPI.Models.SanPham;
using QuanLyBanHangAPI.Services.SanPhamServices;
using QuanLyBanHangAPI.Services.TokenServices;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamServices _sanPhamServices;
        private readonly IWebHostEnvironment _environment;
        private readonly ITokenServices _tokenServices;
        public SanPhamController(ISanPhamServices sanPhamServices, ITokenServices tokenServices,IWebHostEnvironment environment)
        {
            _sanPhamServices = sanPhamServices;
            _tokenServices = tokenServices;
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
            string[] acceptedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(fileName);
            return acceptedExtensions.Contains(extension.ToLower());
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
                return Ok(_sanPhamServices.GetAll());
            }
            catch
            {
                return(StatusCode(StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var sps = _sanPhamServices.GetByID(id);
                if (sps != null)
                {
                    return Ok(sps);
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
        public async Task<IActionResult> Add([FromForm]SanPhamModelDto sanPhamModelDto)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    SanPhamModel sanPhamModel = new SanPhamModel
                    {
                        TenSP = sanPhamModelDto.TenSP,
                        MaGoi = sanPhamModelDto.MaGoi,
                        MaNhaCungCap = sanPhamModelDto.MaNhaCungCap,
                        Gia = sanPhamModelDto.Gia,
                        GiamGia = sanPhamModelDto.GiamGia,
                        TrangThai = sanPhamModelDto.TrangThai,
                        TomTat = sanPhamModelDto.TomTat,
                        MoTa = sanPhamModelDto.MoTa,
                        NoiDung = sanPhamModelDto.NoiDung
                    };


                    if (sanPhamModelDto.TenSP == "" || sanPhamModelDto.TenSP == null)
                    {
                        return BadRequest("Chưa điền tên sản phẩm");
                    }
                    var sp = _sanPhamServices.GetByName(sanPhamModelDto.TenSP);
                    if (sp != null)
                    {
                        return BadRequest("Tên sản phẩm đã tồn tại");
                    }

                    string ngay = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
                    if (sanPhamModelDto.AnhSP != null || sanPhamModelDto.AnhSP.Length != 0)
                    {
                        bool checkAnhSP = IsImage(sanPhamModelDto.AnhSP.FileName);
                        if (checkAnhSP)
                        {
                            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "sanphams", "anhdaidiensanpham", ngay, sanPhamModelDto.TenSP);
                            if (!Directory.Exists(uploadsFolderPath))
                            {
                                Directory.CreateDirectory(uploadsFolderPath);
                            }
                            var filePath = Path.Combine(uploadsFolderPath, sanPhamModelDto.AnhSP.FileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await sanPhamModelDto.AnhSP.CopyToAsync(stream);
                                stream.Flush();
                                sanPhamModel.AnhSP = sanPhamModelDto.AnhSP.FileName;
                            }
                        }
                        else
                        {
                            return BadRequest("Chỉ chấp nhận file ảnh có định dạng (.jpg, .jpeg, .png, .gif)");
                        }
                    }

                    if (sanPhamModelDto.ListAnh != null && sanPhamModelDto.ListAnh.Count > 0)
                    {
                        foreach (var anh in sanPhamModelDto.ListAnh)
                        {
                            if (anh.Length != 0)
                            {
                                bool checkAnhSanPham = IsImage(anh.FileName);
                                if (checkAnhSanPham)
                                {
                                    var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "sanphams", "anhsanpham", ngay, sanPhamModelDto.TenSP);
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
                                    sanPhamModel.ListAnh += anh.FileName + ":";
                                }
                                else
                                {
                                    return BadRequest("Một số file upload không đúng định dạng.Chỉ chấp nhận file ảnh có định dạng (.jpg, .jpeg, .png, .gif)");
                                }
                            }
                        }
                    }

                    return Ok(_sanPhamServices.Add(sanPhamModel));
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
        public async Task<IActionResult> Update(int id, [FromForm]SanPhamModelDto sanPhamModelDto)
        {
            bool check = CheckIsTokenExpired();
            if (check == false)
            {
                try
                {
                    if (sanPhamModelDto.TenSP == null)
                    {
                        return BadRequest("Chưa điền tên sản phẩm");
                    }
                    var sannPhamInDB = _sanPhamServices.GetByID(id);
                    if (sannPhamInDB == null)
                    {
                        return BadRequest("Không tồn tại");
                    }
                    var sanpham = new SanPhamVM
                    {
                        MaSP = id,
                        TenSP = sanPhamModelDto.TenSP,
                        MaGoi = sanPhamModelDto.MaGoi,
                        MaNhaCungCap = sanPhamModelDto.MaNhaCungCap,
                        Gia = sanPhamModelDto.Gia,
                        GiamGia = sanPhamModelDto.GiamGia,
                        TrangThai = sanPhamModelDto.TrangThai,
                        TomTat = sanPhamModelDto.TomTat,
                        MoTa = sanPhamModelDto.MoTa,
                        NoiDung = sanPhamModelDto.NoiDung
                    };

                    string ngay = sannPhamInDB.NgayTao.Day.ToString() + "-" + sannPhamInDB.NgayTao.Month.ToString() + "-" + sannPhamInDB.NgayTao.Year.ToString();
                    if (sanPhamModelDto.AnhSP != null && sanPhamModelDto.AnhSP.Length != 0)
                    {
                        bool checkAnhSP = IsImage(sanPhamModelDto.AnhSP.FileName);
                        if (checkAnhSP)
                        {
                            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "sanphams", "anhdaidiensanpham", ngay, sanPhamModelDto.TenSP);
                            if (!Directory.Exists(uploadsFolderPath))
                            {
                                Directory.CreateDirectory(uploadsFolderPath);
                            }
                            var filePath = Path.Combine(uploadsFolderPath, sanPhamModelDto.AnhSP.FileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await sanPhamModelDto.AnhSP.CopyToAsync(stream);
                                stream.Flush();
                                sanpham.AnhSP = sanPhamModelDto.AnhSP.FileName;
                            }
                        }
                        else
                        {
                            return BadRequest("Chỉ chấp nhận file ảnh có định dạng (.jpg, .jpeg, .png, .gif)");
                        }
                    }
                    else
                    {
                        sanpham.AnhSP = sannPhamInDB.AnhSP;
                    }

                    if (sanPhamModelDto.ListAnh != null && sanPhamModelDto.ListAnh.Count > 0)
                    {
                        foreach (var anh in sanPhamModelDto.ListAnh)
                        {
                            if (anh.Length != 0)
                            {
                                bool checkAnhSanPham = IsImage(anh.FileName);
                                if (checkAnhSanPham)
                                {
                                    var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "images", "sanphams", "anhsanpham", ngay, sanPhamModelDto.TenSP);
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
                                    sanpham.ListAnh += anh.FileName + ":";
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
                        sanpham.ListAnh = sannPhamInDB.ListAnh;
                    }


                    string result = _sanPhamServices.Update(sanpham);
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
