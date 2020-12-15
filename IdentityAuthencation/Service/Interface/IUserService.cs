using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Interface
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterUSerByAdmin(RegisterRequestDto request);
        Task<ApplicationUser> RegisterUSer(RegisterDto request);

        Task<AuthenticateResponseDto> Login(AuthenticateRequestDto request);
        AuthenticateResponseDto RefreshToken(string token);
        bool RevokeToken(string token);

        Task<IEnumerable<ApplicationUser>> FindAll();
        Task<ApplicationUser> FindbyId(Guid UserId);
        Task Update(Guid UserId, UpdateUserRequestDto request);
        Task Delete(Guid UserId);
        Task ChangePassword(string UserName, string currentPassword, string newPassword, string passwordConfirm);
    }
}
