using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IdentityAuthencation.Authorization;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthencation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //Post api/user/registeradmin
        [HttpPost("RegisterAdmin")]
        [Authorize(Roles = "superadministrator")]
        public async Task<IActionResult> RegisterUserByAdmin(RegisterAdminDto request)
        {
            try
            {
                await _userService.RegisterUSerByAdmin(request);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Post api/user/register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(RegisterDto request)
        {
            try
            {
                await _userService.RegisterUSer(request);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Post api/user/login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthenticateRequestDto request)
        {
            try
            {
                var login = await _userService.Login(request);
                setTokenCookie(login.RefreshToken);
                return Ok(login);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _userService.RefreshToken(refreshToken);

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(response.RefreshToken.ToString());

            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromBody] RevokeTokenRequestDto model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _userService.RevokeToken(token);

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        [HttpGet("{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(Guid id)
        {
            var user = _userService.FindbyId(id);
            if (user == null) return NotFound();

            return Ok(user.Result.RefreshTokens);
        }

        //Get api/user
        [HttpGet]
        [Authorize(Permission.Users.View)]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var users = await _userService.FindAll();
                var userDtos = _mapper.Map<IList<UserResponseDto>>(users);

                return Ok(userDtos);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Get api/user/UserId
        [HttpGet("{UserId}")]
        [Authorize(Permission.Users.View)]
        public async Task<IActionResult> FindById(Guid UserId)
        {
            try
            {
                var user = await _userService.FindbyId(UserId);
                var userDto = _mapper.Map<UserResponseDto>(user);

                return Ok(userDto);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //PUT api/user/UserId
        [HttpPut("{UserId}")]
        //[Authorize(Permission.Users.Edit)]
        [AllowAnonymous]
        public async Task<IActionResult> Update(Guid UserId, RegisterDto request)
        {
            try
            {
                await _userService.Update(UserId, request);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Delete api/user/UserId
        [HttpDelete("{UserId}")]
        [Authorize(Permission.Users.Delete)]
        public async Task<IActionResult> Delete(Guid UserId)
        {
            try
            {
                await _userService.Delete(UserId);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // helper methods
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
