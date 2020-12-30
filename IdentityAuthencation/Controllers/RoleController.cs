using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IdentityAuthencation.Authorization;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Service.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthencation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        //GET api/role/GetAll
        [HttpGet("GetAll")]
        [Authorize(Permission.Roles.View)]
        public async Task<IActionResult> GetAllRole()
        {
            var roles = await _roleService.GetAllRole();
            var roleDtos = _mapper.Map<IList<RoleResponseDto>>(roles);

            return Ok(roleDtos);
        }

        //Get api/role/roleId
        [HttpGet("{RoleId}")]
        [Authorize(Permission.Roles.View)]
        public async Task<IActionResult> GetRoleById(Guid RoleId)
        {
            try
            {
                var roles = await _roleService.GetRoleById(RoleId);
                var roleDtos = _mapper.Map<RoleResponseDto>(roles);

                return Ok(roleDtos);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Post api/role/create
        [HttpPost("Create")]
        [Authorize(Permission.Roles.Create)]
        public async Task<IActionResult> CreateRole(RoleRequestDto request)
        {
            try
            {
                await _roleService.CreateRole(request);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Put api/role/roleId
        [HttpPut("{RoleId}")]
        [Authorize(Permission.Roles.Edit)]
        public async Task<IActionResult> UpdateRole(Guid RoleId, RoleRequestDto model)
        {
            try
            {
                await _roleService.UpdateRole(RoleId, model);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Delete api/role/roleId
        [HttpDelete("{RoleId}")]
        [Authorize(Permission.Roles.Delete)]
        public async Task<IActionResult> DeleteRole(Guid RoleId)
        {
            try
            {
                await _roleService.DeleteRole(RoleId);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { meesage = ex.Message });
            }
        }

        [HttpGet("FindRole")]
        [Authorize(Permission.Roles.View)]
        public async Task<IActionResult> FindRole(string Name)
        {
            return Ok(await _roleService.FindRole(Name));
        }

        //Post api/role/addusertorole
        [HttpPost("AddUserToRole")]
        [Authorize(Permission.Roles.Create)]
        public async Task<IActionResult> AddUserToRole(AddToRoleDto model)
        {
            try
            {
                await _roleService.AddUserToRole(model);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Post api/role/addusertorole
        [HttpPost("RemoveUserRole")]
        [Authorize(Permission.Roles.Delete)]
        public async Task<IActionResult> RemoveUserRole(AddToRoleDto model)
        {
            try
            {
                await _roleService.RemoveUserRole(model);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
