using AutoMapper;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.RootService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityAuthencation.Service.Role
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IMapper _mapper;

        public RoleService(ILoggerManager logger, IUnitOfWork unitOfwork, IMapper mapper)
            : base(logger, unitOfwork)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllRole()
        {
            return await ProcessRequest(() =>
            {
                return _unitOfWork.Role.GetAllAsync();
            });
        }

        public async Task<ApplicationRole> GetRoleById(Guid RoleId)
        {
            return await ProcessRequest(() =>
            {
                var role = _unitOfWork.Role.FindByIdAsync(RoleId);

                if (role.Result != null) return role;
                throw new AppException(_logger, $"Role id: {RoleId} not found");
            });

        }
        public async Task<ApplicationRole> CreateRole(RoleRequestDto request)
        {
            return await ProcessRequest(async () =>
            {
                var role = await _unitOfWork.Role.FindByNameAsync(request.Name);
                if (role != null) throw new AppException(_logger, $"Role Name: {request.Name} is already taken");

                role = _mapper.Map<ApplicationRole>(request);

                role.NormalizedName = role.Name.ToUpper();
                role.Name = role.NormalizedName.ToLower();

                await _unitOfWork.Role.CreateAsync(role);
                await _unitOfWork.SaveAsync();

                return role;
            });
        }

        public async Task UpdateRole(Guid RoleId, RoleRequestDto request)
        {
            await ProcessRequest(async () =>
            {
                var role = await _unitOfWork.Role.FindByIdAsync(RoleId);
                if (role == null) throw new AppException(_logger, "Role not found");

                if (!await _unitOfWork.Role.GetByAnyConditionAsync(x => x.Name == request.Name && x.Id != RoleId))
                {
                    if (!string.IsNullOrEmpty(request.Name))
                    {
                        role.Name = request.Name.ToLower();
                    }
                    else
                    {
                        role.Name = role.Name;
                    }
                    if (!string.IsNullOrEmpty(request.Description))
                    {
                        role.Description = request.Description;
                    }
                    else
                    {
                        role.Description = role.Description;
                    }

                    _unitOfWork.Role.Update(role);
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    throw new AppException(_logger, $"Role Name: {request.Name} is already taken");
                }
            });
        }

        public async Task DeleteRole(Guid RoleId)
        {
            await ProcessRequest(async () =>
            {
                var role = await _unitOfWork.Role.FindByIdAsync(RoleId);
                if (role == null) throw new AppException(_logger, $"Role Id: {RoleId} not found");

                _unitOfWork.Role.Delete(role);
                await _unitOfWork.SaveAsync();
            });
        }

        public async Task<IEnumerable<ApplicationRole>> FindRole(string Name)
        {
            return await ProcessRequest(() =>
            {
                return _unitOfWork.Role.GetByWhereConditionAsync(x => x.Name.Contains(Name));
            });
        }

        public async Task AddUserToRole(AddToRoleDto model)
        {
            await ProcessRequest(async () =>
           {
               ApplicationUser existingUser = await _unitOfWork.User.FindByNameAsync(model.UserName);
               if (existingUser == null) throw new AppException(_logger, $"User name: {model.UserName} not found");

               List<ApplicationRole> currentRole = existingUser.UserRoles.Select(x => x.Role).ToList();

               var existingRole = await _unitOfWork.Role.FindByNameAsync(model.RoleName);
               if (existingRole == null)
               {
                   throw new AppException(_logger, $"Role name: {model.RoleName} not found");
               }

               if (currentRole.Select(x => x.Name).Contains(model.RoleName.ToLower()))
               {
                   throw new AppException(_logger, $"User already has an {model.RoleName} role");
               }

               ApplicationUserRole applicationUserRole = new ApplicationUserRole()
               {
                   RoleId = existingRole.Id,
                   UserId = existingUser.Id
               };

               await _unitOfWork.UserRole.CreateAsync(applicationUserRole);
               await _unitOfWork.SaveAsync();
           });
        }
        public async Task RemoveUserRole(AddToRoleDto model)
        {
            await ProcessRequest(async () =>
            {
                ApplicationUser existingUser = await _unitOfWork.User.FindByNameAsync(model.UserName);
                if (existingUser == null) throw new AppException(_logger, $"User name: {model.UserName} not found");

                List<ApplicationRole> currentRole = existingUser.UserRoles.Select(x => x.Role).ToList();

                var existingRole = await _unitOfWork.Role.FindByNameAsync(model.RoleName);
                if (existingRole == null)
                {
                    throw new AppException(_logger, $"Role name: {model.RoleName} not found");
                }

                if (!currentRole.Select(x => x.Name).Contains(model.RoleName.ToLower()))
                {
                    throw new AppException(_logger, $"the user is not in this role: {model.RoleName}");
                }

                var userRole = await _unitOfWork.UserRole.FirstOrDefaultAsync(x => x.RoleId == existingRole.Id && x.UserId == existingUser.Id);
                if (userRole == null) throw new AppException(_logger, $"User name: {model.UserName} not belong role: {model.RoleName}");

                _unitOfWork.UserRole.Delete(userRole);
                await _unitOfWork.SaveAsync();
            });
        }
    }
}
