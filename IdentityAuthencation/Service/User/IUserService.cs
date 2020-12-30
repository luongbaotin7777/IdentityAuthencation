using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.User
{
    public interface IUserService
    {
        /*-----------------------------/
         * Register User
         *----------------------------*/
        Task<ApplicationUser> RegisterUSerByAdmin(RegisterAdminDto request);
        Task<ApplicationUser> RegisterUSer(RegisterDto request);

        /*-----------------------------/
         * Login
         *----------------------------*/
        Task<AuthenticateResponseDto> Login(AuthenticateRequestDto request);
        /*-----------------------------/
         * Refresh & Revoke token
         *----------------------------*/
        AuthenticateResponseDto RefreshToken(string token);
        bool RevokeToken(string token);

        /*-----------------------------/
         * Find, Update, Delete User
         *----------------------------*/
        Task<IEnumerable<ApplicationUser>> FindAll();
        Task<ApplicationUser> FindbyId(Guid UserId);
        Task Update(Guid UserId, RegisterDto request);
        Task Delete(Guid UserId);
    }
}
