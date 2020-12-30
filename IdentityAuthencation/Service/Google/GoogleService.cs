using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.Role;
using IdentityAuthencation.Service.RootService;
using IdentityAuthencation.Service.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Google
{
    public class GoogleService : BaseService, IGoogleService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IRoleService _roleService;

        public GoogleService(IUnitOfWork unitOfWork, ILoggerManager logger, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IRoleService roleService)
        : base(logger, unitOfWork)
        {
            _signInManager = signInManager;
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
            return await ProcessRequest(async () =>
           {
               ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
               if (info == null) throw new AppException(_logger, "Failed to get information");

               ApplicationUser userEmailExists = await _unitOfWork.User.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
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

                   await _unitOfWork.User.CreateAsync(appUser);
                   await _unitOfWork.SaveAsync();

                   List<Claim> claims = new List<Claim>()
                       {
                        new Claim(ClaimTypes.GivenName,appUser.FirstName),
                        new Claim(ClaimTypes.Surname,appUser.LastName),
                        new Claim(ClaimTypes.Email,appUser.Email),
                       };

                   AddToRoleDto role = new AddToRoleDto()
                   {
                       RoleName = "USER",
                       UserName = appUser.UserName
                   };

                   await _roleService.AddUserToRole(role);
                   foreach (var claim in claims)
                   {
                       var userClaim = new ApplicationUserClaim()
                       {
                           UserId = appUser.Id,
                           ClaimType = claim.Type,
                           ClaimValue = claim.Value
                       };

                       await _unitOfWork.UserClaim.CreateAsync(userClaim);
                   }

                   await _unitOfWork.SaveAsync();
                   return _tokenService.GenerateJWTToken(appUser, 1);
               }
               else
               {
                   userEmailExists.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                   userEmailExists.FirstName = info.Principal.FindFirst(ClaimTypes.GivenName).Value;
                   userEmailExists.LastName = info.Principal.FindFirst(ClaimTypes.Surname).Value;
                   userEmailExists.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                   userEmailExists.EmailConfirmed = true;

                   _unitOfWork.User.Update(userEmailExists);
                   await _unitOfWork.SaveAsync();

                   return _tokenService.GenerateJWTToken(userEmailExists, 1);
               }
           });


        }
    }
}
