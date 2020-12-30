using IdentityAuthencation.Entities;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.RootService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityAuthencation.Service.Token
{
    public class TokenService : BaseService, ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IUnitOfWork unitOfWork, ILoggerManager logger, IConfiguration configuration)
            : base(logger, unitOfWork)
        {
            _configuration = configuration;
        }

        public string GenerateJWTToken(ApplicationUser user, int expMinute)
        {
            SymmetricSecurityKey authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwts:Key"]));

            var existingUser = _unitOfWork.User.FindByName(user.UserName);

            List<ApplicationRole> currentRole = existingUser.UserRoles.Select(x => x.Role).ToList();

            List<Claim> claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName,user.FirstName),
                    new Claim(ClaimTypes.Surname,user.LastName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,string.Join(";",currentRole))
                };

            JwtSecurityToken token = new JwtSecurityToken(
                       claims: claim,
                       notBefore: DateTime.UtcNow,
                       expires: DateTime.UtcNow.AddMinutes(expMinute),
                       signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                   );

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }

        public RefreshToken GenerateRefreshJWTToken(ApplicationUser user)
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            var randomBytes = new byte[64];

            rngCryptoServiceProvider.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(30),
                Created = DateTime.UtcNow,
            };
        }
    }
}

