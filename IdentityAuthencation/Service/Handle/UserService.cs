
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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Handle
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IRoleService _roleService;
        private IMapper _mapper;
        public UserService(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IPasswordHasher<ApplicationUser> passwordHasher, IRoleService roleService, ITokenService tokenService)
            : base(logger, unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _roleService = roleService;
            _tokenService = tokenService;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ApplicationUser>> FindAll()
        {
            return await ProcessRequest(() =>
            {
                return _unitOfWork.User.GetAllUser();
            });
        }

        public async Task<ApplicationUser> FindbyId(Guid UserId)
        {
            return await ProcessRequest(async () =>
            {
                var user = await _unitOfWork.User.GetUserById(UserId);
                if (user == null) throw new AppException(_logger, $"User Id: {UserId} not found");

                return user;
            });
        }

        public async Task<AuthenticateResponseDto> Login(AuthenticateRequestDto request)
        {
            return await ProcessRequest(async () =>
             {
                 var user = await _userManager.FindByNameAsync(request.UserName);
                 if (user == null) throw new AppException(_logger, $"User: {request.UserName} not found");

                 var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);
                 if (!result.Succeeded) throw new AppException(_logger, $"Login Failed");

                 var jwtToken = _tokenService.GenerateJWTToken(user, 5);
                 var refreshToken = _tokenService.GenerateRefreshJWTToken(user);

                 user.RefreshTokens.Add(refreshToken);
                 
                 _unitOfWork.User.Update(user);
                 await _unitOfWork.SaveAsync();

                 return new AuthenticateResponseDto(user, jwtToken, refreshToken.Token);
             });
        }

        public AuthenticateResponseDto RefreshToken(string token)
        {
            var user = _unitOfWork.User.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = _tokenService.GenerateRefreshJWTToken(user);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();

            // generate new jwt
            var jwtToken = _tokenService.GenerateJWTToken(user, 5);

            return new AuthenticateResponseDto(user, jwtToken, newRefreshToken.Token);
        }

        public bool RevokeToken(string token)
        {
            var user = _unitOfWork.User.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();

            return true;
        }
        public async Task<ApplicationUser> RegisterUSer(RegisterDto request)
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
                return user;
            });
        }

        public async Task<ApplicationUser> RegisterUSerByAdmin(RegisterRequestDto request)
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
                    var role = new AddToRoleDto()
                    {
                        Name = "USER",
                        UserName = request.UserName
                    };
                    await _roleService.AddUserToRole(role);
                }

                foreach (var role in request.ListRoleName)
                {
                    var roleModel = new AddToRoleDto()
                    {
                        UserName = request.UserName,
                        Name = role
                    };
                    listRoleModel.Add(roleModel);
                }

                if (listRoleModel == null) throw new AppException(_logger, "Add Role Failed");
                foreach (AddToRoleDto roleModel in listRoleModel)
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
                if (string.IsNullOrEmpty(UserId.ToString())) throw new AppException(_logger, $"Please enter UserId");
                var user = await _unitOfWork.User.FindByIdAsync(UserId);
                if (user == null) throw new AppException(_logger, $"User Id: {UserId} not found");
                _unitOfWork.User.Delete(user);
                await _unitOfWork.SaveAsync();
            });
        }

        public async Task ChangePassword(string UserName, string currentPassword, string newPassword, string passwordConfirm)
        {
            await ProcessRequest(async () =>
            {
                if (string.IsNullOrEmpty(UserName)) throw new AppException(_logger, $"Please enter UserName");
                if (string.IsNullOrWhiteSpace(currentPassword)) throw new AppException(_logger, $"Please enter currentPassword, not White Space");
                if (string.IsNullOrWhiteSpace(passwordConfirm)) throw new AppException(_logger, $"Please enter passwordConfirm, not White Space");
                if (string.IsNullOrWhiteSpace(newPassword)) throw new AppException(_logger, $"Please enter newPassword, not White Space");

                var user = await _userManager.FindByNameAsync(UserName);
                if (user == null) throw new AppException(_logger, $"User Name: {UserName} not found");

                if (newPassword != passwordConfirm) throw new AppException(_logger, "The confirmation password is not the same as the password");

                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!result.Succeeded) throw new AppException(_logger, $"Incorrect Current Password,Please try again");
            });
        }
    }
}
