using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Data.DTO;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public UserController(UserManager<ApplicationUser> userManager,IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
        }
        private bool IsValidEmail(string email)
        {
            // Định nghĩa biểu thức chính quy cho định dạng địa chỉ email
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Tạo một đối tượng Regex với biểu thức chính quy đã định nghĩa
            Regex regex = new Regex(pattern);

            // Kiểm tra xem địa chỉ email có khớp với biểu thức chính quy hay không
            return regex.IsMatch(email);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var checkusername = await _userManager.FindByNameAsync(registerDto.UserName);
            if (checkusername != null)
            {
                return BadRequest("UserName đã tồn tại");
            }
            bool checkmail = IsValidEmail(registerDto.Email);
            if (checkmail == false)
            {
                return BadRequest("Email sai định dạng");
            }
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
            {
                return BadRequest("Email đã được đăng ký");
            }          

            var adduser = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FullName = registerDto.UserName
                
            };

            var result = await _userManager.CreateAsync(adduser, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
        [HttpPost("updateprofile")]
        [Authorize]
        public async Task<IActionResult> UpdateTTUser([FromForm] UpdateUserDto updateUserDto)
        {
            if (updateUserDto.userName == null)
            {
                return BadRequest("chưa điền thông tin");
            }
            var user = await _userManager.FindByNameAsync(updateUserDto.userName);
            if (user == null)
            {
                return NotFound("User không tồn tại");
            }
            if (updateUserDto.email != null)
            {
                user.Email = updateUserDto.email;
            }
            if (updateUserDto.fullName != null)
            {
                user.FullName = updateUserDto.fullName;
            }
            if (updateUserDto.avatarUrl != null || updateUserDto.avatarUrl.Length != 0)
            {
                var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads", "avatar", user.UserName);
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                // Xóa file avatar cũ
                string fileOldPath = Path.Combine(_environment.WebRootPath, "uploads", "avatar", user.UserName,user.AvatarUrl);
                if (fileOldPath != null)
                {
                    System.IO.File.Delete(fileOldPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, updateUserDto.avatarUrl.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateUserDto.avatarUrl.CopyToAsync(stream);
                    stream.Flush();
                    user.AvatarUrl = updateUserDto.avatarUrl.FileName;
                }
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }          
            return Ok();
        }

        [HttpPost("reset-password")]
        [Authorize(Roles ="Ad")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            if (resetPassword.newPass == null)
            {
                return BadRequest("Mật khẩu mới không được để trống");
            }
            if (resetPassword.userName == null)
            {
                return BadRequest("user name không được để trống");
            }
            if (resetPassword.newPass.Length < 4)
            {
                return BadRequest("Mật khẩu mới quá ngắn");
            }
            var user = await _userManager.FindByNameAsync(resetPassword.userName);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(resetPassword.userName);
            }

            if (user == null)
            {
                return BadRequest("User không tồn tại");
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPassword.newPass);

            if (result.Succeeded)
            {
                return Ok("Đặt lại mật khẩu thành công");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Authorize(Roles ="Ad")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ApplicationUser>> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{username}")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordUser(ChangePassDto changePassDto)
        {
            if (changePassDto.userName == null)
            {
                return BadRequest("userName không được để trống");
            }
            if (changePassDto.currentPass == null)
            {
                return BadRequest("Mật khẩu hiện tại không được để trống");
            }
            if (changePassDto.newPass == null)
            {
                return BadRequest("Mật khẩu mới không được để trống");
            }
            var user = await _userManager.FindByNameAsync(changePassDto.userName);

            if (user == null)
            {
                return NotFound();
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, changePassDto.currentPass);
            if (isPasswordCorrect)
            {
                if (changePassDto.comfirmPass == changePassDto.newPass)
                {
                    var result = await _userManager.ChangePasswordAsync(user, changePassDto.currentPass, changePassDto.comfirmPass);
                    if (result.Succeeded)
                    {
                        return Ok("Đổi mật khẩu thành công");
                    }
                    return BadRequest(result.Errors);
                }
                return BadRequest("Mật khẩu nhập lại không đúng");
            }
            return BadRequest("Mật khẩu cũ không đúng");
        }

        [HttpDelete("{username}")]
        [Authorize(Roles ="Ad")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
        
    }
}
