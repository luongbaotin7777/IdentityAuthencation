using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Role
{
    public interface IRoleService
    {
        /*-----------------------------/
         * CRUD Role
         *----------------------------*/
        Task<ApplicationRole> CreateRole(RoleRequestDto request);
        Task<IEnumerable<ApplicationRole>> GetAllRole();
        Task<ApplicationRole> GetRoleById(Guid RoleId);
        Task UpdateRole(Guid RoleId, RoleRequestDto request);
        Task DeleteRole(Guid RoleId);
        Task<IEnumerable<ApplicationRole>> FindRole(string Name);

        /*-----------------------------/
         * Add & Remove UserRole
         *----------------------------*/
        Task AddUserToRole(AddToRoleDto model);
        Task RemoveUserRole(AddToRoleDto model);
    }
}
