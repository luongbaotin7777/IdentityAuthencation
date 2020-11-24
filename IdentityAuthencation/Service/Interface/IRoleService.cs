using IdentityAuthencation.Common;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Interface
{
    public interface IRoleService
    {
        Task<ApplicationRole> CreateRole(CreateRoleRequestDtos request);
        Task<IEnumerable<ApplicationRole>> GetAllRole();
        Task<ApplicationRole> GetRoleById(Guid RoleId);
        Task UpdateRole(Guid RoleId, CreateRoleRequestDtos request);
        Task DeleteRole(Guid RoleId);
        Task<IEnumerable<ApplicationRole>> FindRole(string Name);
        Task AddUserToRole(AddToRoleModel model);
        Task RemoveUserRole(AddToRoleModel model);
    }
}
