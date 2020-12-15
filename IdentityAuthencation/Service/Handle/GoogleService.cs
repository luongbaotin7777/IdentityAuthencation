using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.Interface;
using IdentityAuthencation.Service.RootService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Handle
{
    public class GoogleService :BaseService, IGoogleService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IRoleService _roleService;
        public GoogleService(IUnitOfWork unitOfWork,ILoggerManager logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenService tokenService, IRoleService roleService)
        :base(logger,unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _roleService = roleService;
           
        }
        public ChallengeResult GoogleLogin()
        {
                string redirectUrl = "/api/signin-google";
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
                return new ChallengeResult("Google", properties);      
        }
        public async Task<string> ExternalLoginCallback()
        {
            return await ProcessRequest( async() =>
            {
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null) throw new AppException(_logger, "Failed to get information");
                ApplicationUser userEmailExists = await _userManager.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
                if (userEmailExists == null)
                {
                    ApplicationUser appUser = new ApplicationUser()
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        FirstName = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        LastName = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        EmailConfirmed = true,
                    };

                    IdentityResult identityResult = await _userManager.CreateAsync(appUser);
                    if (!identityResult.Succeeded) throw new AppException(_logger, "Failed to create User");
                    List<Claim> claims = new List<Claim>()
                        {

                        new Claim(ClaimTypes.GivenName,appUser.FirstName),
                        new Claim(ClaimTypes.Surname,appUser.LastName),
                        new Claim(ClaimTypes.Email,appUser.Email),
                        };
                    AddToRoleDto role = new AddToRoleDto()
                    {
                        Name = "USER",
                        UserName = appUser.UserName
                    };
                    await _roleService.AddUserToRole(role);
                    await _userManager.AddClaimsAsync(appUser, claims);
                    return await _tokenService.GenerateJWTToken(appUser, 1);
                }
                else
                {
                    userEmailExists.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    userEmailExists.FirstName = info.Principal.FindFirst(ClaimTypes.GivenName).Value;
                    userEmailExists.LastName = info.Principal.FindFirst(ClaimTypes.Surname).Value;
                    userEmailExists.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    userEmailExists.EmailConfirmed = true;
                    IdentityResult identityResult = await _userManager.UpdateAsync(userEmailExists);
                    if (!identityResult.Succeeded) throw new AppException(_logger, "Failed to Update User");
                    return await _tokenService.GenerateJWTToken(userEmailExists, 1);
                }
            });
           

        }
    }
}
