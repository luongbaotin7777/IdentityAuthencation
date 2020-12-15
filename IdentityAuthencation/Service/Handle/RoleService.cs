using AutoMapper;
using IdentityAuthencation.Authorization;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.Interface;
using IdentityAuthencation.Service.RootService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityAuthencation.Service.Handle
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RoleService(ILoggerManager logger, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfwork, IMapper mapper)
            : base(logger, unitOfwork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
        public async Task<ApplicationRole> CreateRole(CreateRoleRequestDto request)
        {
            return await ProcessRequest(async () =>
            {
                var role = await _roleManager.FindByNameAsync(request.Name);
                if (role != null) throw new AppException(_logger, $"Role Name: {request.Name} is already taken");

                role = _mapper.Map<ApplicationRole>(request);

                role.NormalizedName = role.Name.ToUpper();
                role.Name = role.NormalizedName.ToLower();
                await _unitOfWork.Role.CreateAsync(role);
                await _unitOfWork.SaveAsync();

                return role;
            });
        }
        public async Task UpdateRole(Guid RoleId, CreateRoleRequestDto request)
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
               var user = await _userManager.FindByNameAsync(model.UserName);
               if (user == null) throw new AppException(_logger, $"User name: {model.UserName} not found");

               var role = await _roleManager.FindByNameAsync(model.Name);
               if (role == null) throw new AppException(_logger, $"Role name: {model.Name} not found");

               var userRole = await _userManager.GetRolesAsync(user);
               bool checkrole = userRole.ToList().Contains(model.Name.ToLower());

               if (checkrole) throw new AppException(_logger, $"User name: {model.UserName} already belong to Role name: {model.Name}");
               var result = await _userManager.AddToRoleAsync(user, role.Name);
               if (!result.Succeeded) throw new AppException(_logger, "Add User to Role failed");
           });
        }
        public async Task RemoveUserRole(AddToRoleDto model)
        {
            await ProcessRequest(async () =>
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null) throw new AppException(_logger, $"User name: {model.UserName} not found");

                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role == null) throw new AppException(_logger, $"Role name: {model.Name} not found");

                var userRole = await _userManager.GetRolesAsync(user);
                bool checkrole = userRole.ToList().Contains(model.Name.ToLower());
                if (!checkrole) throw new AppException(_logger, $"User name: {model.UserName} is not in Role name: {model.Name}");

                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!result.Succeeded) throw new AppException(_logger, "Remove User from Role failed");
            });
        }
    }
}
