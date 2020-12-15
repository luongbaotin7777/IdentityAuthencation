
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Interface
{
    public interface IRoleService
    {
        Task<ApplicationRole> CreateRole(CreateRoleRequestDto request);
        Task<IEnumerable<ApplicationRole>> GetAllRole();
        Task<ApplicationRole> GetRoleById(Guid RoleId);
        Task UpdateRole(Guid RoleId, CreateRoleRequestDto request);
        Task DeleteRole(Guid RoleId);
        Task<IEnumerable<ApplicationRole>> FindRole(string Name);
        Task AddUserToRole(AddToRoleDto model);
        Task RemoveUserRole(AddToRoleDto model);
    }
}
