using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Data.DTO;
using QuanLyBanHangAPI.Services.TokenServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenServices _tokenServices;
        private readonly DB _dB;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, DB dB, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dB = dB;
            _tokenServices = tokenServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName",user.UserName),
                    new Claim(JwtRegisteredClaimNames.UniqueName,user.FullName),
                    //new Claim("Avatar",user.AvatarUrl)
                };
                if (user.AvatarUrl != null || user.AvatarUrl == "")
                {
                    authClaims.Add(new Claim("Avatar", user.AvatarUrl));
                }
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                // Lưu token vào db

                var tokendb = new Token
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    TokenKey = new JwtSecurityTokenHandler().WriteToken(token),
                    CreateAt = DateTime.UtcNow,
                    VaiLidTo = token.ValidTo,
                    TokenIsUsed = true,
                    TokenIsReVoked = false,
                    ReFreshToken = _tokenServices.GenerateRefreshToken(),
                    IsUsed = false,
                    IsRevoked = false
                };

                await _dB.AddAsync(tokendb);
                await _dB.SaveChangesAsync();
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    reFreshToken = tokendb.ReFreshToken
                });
            }
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
