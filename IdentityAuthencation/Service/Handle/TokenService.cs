﻿
using IdentityAuthencation.Common;
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
        public async Task<string> GenerateJWTToken(string UserName, int expDay)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwts:Key"]));
            var userRoles = await _userManager.GetRolesAsync(user);
            var claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName,user.FirstName),
                    new Claim(ClaimTypes.Surname,user.LastName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,string.Join(";",userRoles)),

                };
            var token = new JwtSecurityToken(
                       claims: claim,
                       expires: DateTime.UtcNow.AddDays(expDay),
                       signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                   );
           return new JwtSecurityTokenHandler().WriteToken(token);
          
        }


        //var token = tokenHandler.CreateToken(tokenDescriptor);


    }
}

