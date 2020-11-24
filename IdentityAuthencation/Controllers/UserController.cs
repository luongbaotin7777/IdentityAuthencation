using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Service.Interface;
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
        //Post api/user/register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(RegisterRequestDto request)
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
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var login = await _userService.Login(request);
                return Ok(login);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //Get api/user
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {

            var users = await _userService.FindAll();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return Ok(userDtos);
        }
        //Get api/user/UserId
        [HttpGet("{UserId}")]
        public async Task<IActionResult> FindById(Guid UserId)
        {
            try
            {
                var user = await _userService.FindbyId(UserId);
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);

            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //PUT api/user/UserId
        [HttpPut("{UserId}")]
        public async Task<IActionResult> Update(Guid UserId, UpdateUserRequestDto request)
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
    }
}
