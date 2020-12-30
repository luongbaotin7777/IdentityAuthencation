using AutoMapper;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Service.Role;
using IdentityAuthencation.Service.RootService;
using IdentityAuthencation.Service.Token;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IRoleService _roleService;
        private IMapper _mapper;

        public UserService(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper, IRoleService roleService, ITokenService tokenService)
            : base(logger, unitOfWork)
        {
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
                 var user = await _unitOfWork.User.FindByNameAsync(request.UserName);
                 if (user == null) throw new AppException(_logger, $"User: {request.UserName} not found");

                 if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) throw new AppException(_logger, $"Login Failed");

                 var jwtToken = _tokenService.GenerateJWTToken(user, 5);
                 var refreshToken = _tokenService.GenerateRefreshJWTToken(user);

                 user.RefreshTokens.Add(refreshToken);

                 refreshToken.AccessToken = jwtToken;

                 _unitOfWork.User.Update(user);
                 await _unitOfWork.SaveAsync();

                 return new AuthenticateResponseDto(user, jwtToken, refreshToken.Token);
             });
        }

        public async Task<ApplicationUser> RegisterUSer(RegisterDto request)
        {
            return await ProcessRequest(async () =>
            {
                if (request.Password != request.ConfirmPassword) throw new AppException(_logger, "ConfirmPassword  is not the same Password");

                var userName = await _unitOfWork.User.FindByNameAsync(request.UserName);
                if (userName != null) throw new AppException(_logger, $"User Name: {request.UserName} is already taken");

                var userEmail = await _unitOfWork.User.FindByEmailAsync(request.Email);
                if (userEmail != null) throw new AppException(_logger, $"User Email: {request.Email} is already taken");

                if (!request.Dob.HasValue) throw new AppException(_logger, "Date of birth is required");

                var user = _mapper.Map<ApplicationUser>(request);

                if (user == null) throw new AppException(_logger, "Register Failed");

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.NormalizedUserName = request.UserName.ToUpper();
                user.NormalizedEmail = request.Email.ToUpper();
                user.SecurityStamp = Guid.NewGuid().ToString("D");

                await _unitOfWork.User.CreateAsync(user);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.GivenName,request.FirstName),
                    new Claim(ClaimTypes.Surname,request.LastName),
                    new Claim(ClaimTypes.Email,request.Email)
                };

                foreach (var claim in claims)
                {
                    var userClaim = new ApplicationUserClaim()
                    {
                        UserId = user.Id,
                        ClaimType = claim.Type,
                        ClaimValue = claim.Value
                    };

                    await _unitOfWork.UserClaim.CreateAsync(userClaim);
                }

                await _unitOfWork.SaveAsync();

                return user;
            });
        }
        public async Task<ApplicationUser> RegisterUSerByAdmin(RegisterAdminDto request)
        {
            return await ProcessRequest(async () =>
            {
                if (request.Password != request.ConfirmPassword) throw new AppException(_logger, "ConfirmPassword  is not the same Password");

                var userName = await _unitOfWork.User.FindByNameAsync(request.UserName);
                if (userName != null) throw new AppException(_logger, $"User Name: {request.UserName} is already taken");

                var userEmail = await _unitOfWork.User.FindByEmailAsync(request.Email);
                if (userEmail != null) throw new AppException(_logger, $"User Email: {request.Email} is already taken");

                if (!request.Dob.HasValue) throw new AppException(_logger, "Date of birth is required");

                var user = _mapper.Map<ApplicationUser>(request);

                if (user == null) throw new AppException(_logger, "Register Failed");

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.NormalizedUserName = request.UserName.ToUpper();
                user.NormalizedEmail = request.Email.ToUpper();
                user.SecurityStamp = Guid.NewGuid().ToString("D");

                await _unitOfWork.User.CreateAsync(user);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.GivenName,request.FirstName),
                    new Claim(ClaimTypes.Surname,request.LastName),
                    new Claim(ClaimTypes.Email,request.Email)
                };

                foreach (var claim in claims)
                {
                    var userClaim = new ApplicationUserClaim()
                    {
                        UserId = user.Id,
                        ClaimType = claim.Type,
                        ClaimValue = claim.Value
                    };

                    await _unitOfWork.UserClaim.CreateAsync(userClaim);
                }

                await _unitOfWork.SaveAsync();
                var listRoleModel = new ArrayList();

                if (request.ListRoleName == null)
                {
                    var role = new AddToRoleDto()
                    {
                        RoleName = "USER",
                        UserName = request.UserName
                    };
                    await _roleService.AddUserToRole(role);
                }
                else
                {
                    foreach (var role in request.ListRoleName)
                    {
                        var roleModel = new AddToRoleDto()
                        {
                            UserName = request.UserName,
                            RoleName = role
                        };
                        listRoleModel.Add(roleModel);
                    }
                }

                if (listRoleModel == null) throw new AppException(_logger, "Add Role Failed");
                foreach (AddToRoleDto roleModel in listRoleModel)
                {
                    await _roleService.AddUserToRole(roleModel);
                }

                return user;
            });
        }

        public async Task Update(Guid UserId, RegisterDto request)
        {
            await ProcessRequest(async () =>
            {
                var user = await _unitOfWork.User.FindByIdAsync(UserId);
                if (user == null) throw new AppException(_logger, $"User Id: {UserId} not found");

                if (!await _unitOfWork.User.GetByAnyConditionAsync(x => x.UserName == request.UserName && x.Id != UserId))
                {
                    if (!string.IsNullOrEmpty(request.UserName))
                    {
                        user.UserName = request.UserName;
                        user.NormalizedUserName = request.UserName.ToUpper();
                    }
                    else
                    {
                        user.UserName = user.UserName;
                    }
                    if (!string.IsNullOrEmpty(request.FirstName))
                    {
                        user.FirstName = request.FirstName;
                    }
                    else
                    {
                        user.FirstName = user.FirstName;
                    }
                    if (!string.IsNullOrEmpty(request.LastName))
                    {
                        user.LastName = request.LastName;
                    }
                    else
                    {
                        user.LastName = user.LastName;
                    }
                    if (!string.IsNullOrEmpty(request.PhoneNumber))
                    {
                        user.PhoneNumber = request.PhoneNumber;
                    }
                    else
                    {
                        user.PhoneNumber = user.PhoneNumber;
                    }
                    if (request.Dob.HasValue)
                    {
                        user.Dob = request.Dob;
                    }
                    else
                    {
                        user.Dob = user.Dob;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Password))
                    {
                        byte[] passwordHash, passwordSalt;
                        CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                        user.PasswordHash = passwordHash;
                        user.PasswordSalt = passwordSalt;
                    }

                    _unitOfWork.User.Update(user);
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

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");

            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public AuthenticateResponseDto RefreshToken(string token)
        {
            var user = _unitOfWork.User.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // generate new jwt
            var jwtToken = _tokenService.GenerateJWTToken(user, 5);

            // replace old refresh token with a new one and save
            var newRefreshToken = _tokenService.GenerateRefreshJWTToken(user);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            user.RefreshTokens.Add(newRefreshToken);

            newRefreshToken.AccessToken = jwtToken;

            _unitOfWork.User.Update(user);
            _unitOfWork.Save();

            return new AuthenticateResponseDto(user, jwtToken.ToString(), newRefreshToken.Token);
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
    }
}
