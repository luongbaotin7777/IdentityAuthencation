
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.Interface;
using IdentityAuthencation.Service.RootService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Handle
{
    public class TokenService : BaseService, ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenService(IUnitOfWork unitOfWork, ILoggerManager logger, IConfiguration configuration, UserManager<ApplicationUser> userManager)
            : base(logger, unitOfWork)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateJWTToken(ApplicationUser user, int expMinute)
        {
            SymmetricSecurityKey authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwts:Key"]));
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName,user.FirstName),
                    new Claim(ClaimTypes.Surname,user.LastName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,string.Join(";",userRoles))
                };

            JwtSecurityToken token = new JwtSecurityToken(
                       claims: claim,
                       notBefore: DateTime.UtcNow,
                       expires: DateTime.UtcNow.AddMinutes(expMinute),
                       signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                   );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshJWTToken(ApplicationUser user)
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            var jwtToken = GenerateJWTToken(user, 5);
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                AccessToken = jwtToken,
                Expires = DateTime.UtcNow.AddDays(30),
                Created = DateTime.UtcNow,
            };
        }
    }
}

