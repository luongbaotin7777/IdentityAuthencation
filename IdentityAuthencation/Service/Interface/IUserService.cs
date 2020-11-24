using IdentityAuthencation.Common;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Interface
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterUSer(RegisterRequestDto request);
        Task<string> Login(LoginRequestDto request);
        Task<IEnumerable<ApplicationUser>> FindAll();
        Task<ApplicationUser> FindbyId(Guid UserId);
        Task Update(Guid UserId, UpdateUserRequestDto request);
        Task Delete(Guid UserId);
    }
}
