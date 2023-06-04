using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.Token;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyBanHangAPI.Services.TokenServices
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly DB _db;
        public TokenServices(IConfiguration configuration,DB db)
        {
            _configuration = configuration;
            _db = db;
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public TokenVM GetByToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsTokenExpired(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            try
            {
                // Validate token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(tokenString, tokenValidationParameters, out validatedToken);

                // Check expiration time
                var expirationTime = DateTime.UtcNow.AddSeconds(10);
                if (validatedToken.ValidTo < expirationTime)
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }

            return false;
        }

        public bool SetTokenExprired(string token)
        {
            var tokenindb = _db.Tokens.SingleOrDefault(n => n.TokenKey == token);
            if (tokenindb != null)
            {
                tokenindb.IsRevoked = true;
                tokenindb.TokenIsReVoked = true;
                _db.Update(tokenindb);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public void Update(TokenVM vm)
        {
            var token = _db.Tokens.SingleOrDefault(n => n.TokenKey == vm.TokenKey);
            if (token != null)
            {
                token.TokenIsReVoked = vm.TokenIsReVoked;
                token.IsRevoked = vm.IsRevoked;
                _db.SaveChanges();
            }
        }
    }
}
