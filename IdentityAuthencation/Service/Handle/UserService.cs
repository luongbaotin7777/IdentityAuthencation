
using AutoMapper;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.Interface;
using IdentityAuthencation.Service.RootService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Handle
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IRoleService _roleService;
        private IMapper _mapper;
        public UserService(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IPasswordHasher<ApplicationUser> passwordHasher, IRoleService roleService, ITokenService tokenService)
            : base(logger, unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _roleService = roleService;
            _tokenService = tokenService;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ApplicationUser>> FindAll()
        {
            return await ProcessRequest(() =>
            {
                return _unitOfWork.User.GetAllAsync();
            });
        }

        public async Task<ApplicationUser> FindbyId(Guid UserId)
        {
            return await ProcessRequest(async () =>
            {
                var user = await _unitOfWork.User.FindByIdAsync(UserId);
                if (user == null) throw new AppException(_logger,$"User Id: {UserId} not found");
                return user;
            });
        }

        public async Task<string> Login(LoginRequestDto request)
        {
            return await ProcessRequest(async () =>
             {
                 var userName = await _userManager.FindByNameAsync(request.UserName);
                 if (userName == null) throw new AppException(_logger, $"User Name: {request.UserName} not found");
                 var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);
                 if (!result.Succeeded) throw new AppException(_logger, $"Login Failed");
                 return await _tokenService.GenerateJWTToken(request.UserName, 7);


             });
        }
        public async Task<ApplicationUser> RegisterUSer(RegisterRequestDto request)
        {
            return await ProcessRequest(async () =>
            {
                if (request.Password != request.ConfirmPassword) throw new AppException(_logger, "ConfirmPassword  is not the same Password");
                var userName = await _userManager.FindByNameAsync(request.UserName);
                if (userName != null) throw new AppException(_logger, $"User Name: {request.UserName} is already taken");
                var userEmail = await _userManager.FindByEmailAsync(request.Email);
                if (userEmail != null) throw new AppException(_logger, $"User Email: {request.Email} is already taken");
                if (!request.Dob.HasValue) throw new AppException(_logger, "Date of birth is required");
                var user = _mapper.Map<ApplicationUser>(request);
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) throw new AppException(_logger, "Register Failed");
                var claims = new List<Claim>()
                {

                    new Claim(ClaimTypes.GivenName,request.FirstName),
                    new Claim(ClaimTypes.Surname,request.LastName),
                    new Claim(ClaimTypes.Email,request.Email),
                };
                await _userManager.AddClaimsAsync(user, claims);

                var listRoleModel = new ArrayList();
                if (request.ListRoleName == null)
                {
                    var role = new AddToRoleModel()
                    {
                        RoleName = "USER",
                        UserName = request.UserName
                    };
                    await _roleService.AddUserToRole(role);
                }
                foreach (var role in request.ListRoleName)
                {
                    var roleModel = new AddToRoleModel()
                    {
                        UserName = request.UserName,
                        RoleName = role
                    };
                    listRoleModel.Add(roleModel);
                }
                if (listRoleModel == null) throw new AppException(_logger, "Add Role Failed");
                foreach (AddToRoleModel roleModel in listRoleModel)
                {
                    await _roleService.AddUserToRole(roleModel);
                }
                return user;
            });

        }

        public async Task Update(Guid UserId, UpdateUserRequestDto request)
        {
            await ProcessRequest(async () =>
            {
                var users = await _unitOfWork.User.FindByIdAsync(UserId);
                if (users == null) throw new AppException(_logger, $"User Id: {UserId} not found");
                if (!await _unitOfWork.User.GetByAnyConditionAsync(x => x.UserName == request.UserName && x.Id != UserId))
                {
                    if (!string.IsNullOrEmpty(request.UserName))
                    {
                        users.UserName = request.UserName;
                    }
                    else
                    {
                        users.UserName = users.UserName;
                    }
                    if (!string.IsNullOrEmpty(request.FirstName))
                    {
                        users.FirstName = request.FirstName;
                    }
                    else
                    {
                        users.FirstName = users.FirstName;
                    }
                    if (!string.IsNullOrEmpty(request.LastName))
                    {
                        users.LastName = request.LastName;
                    }
                    else
                    {
                        users.LastName = users.LastName;
                    }
                    if (!string.IsNullOrEmpty(request.PhoneNumber))
                    {
                        users.PhoneNumber = request.PhoneNumber;
                    }
                    else
                    {
                        users.PhoneNumber = users.PhoneNumber;
                    }
                    if (request.Dob.HasValue)
                    {
                        users.Dob = request.Dob;
                    }
                    else
                    {
                        users.Dob = users.Dob;
                    }
                     _unitOfWork.User.Update(users);
                    await _unitOfWork.SaveAsync();
                   
                }
                else
                {
                    throw new AppException(_logger, $"User Name: {request.UserName} is already taken");
                }
               
            });
        }
        public async Task Delete(Guid UserId)
        {
            await ProcessRequest(async () =>
            {
                var user = await _unitOfWork.User.FindByIdAsync(UserId);
                if (user == null) throw new AppException(_logger, $"User Id: {UserId} not found");
                _unitOfWork.User.Delete(user);
                await _unitOfWork.SaveAsync();
            });
        }
    }
}
