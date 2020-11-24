using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Service.Interface;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAllRole()
        {
            var roles = await _roleService.GetAllRole();
            var roleDtos = _mapper.Map<IList<RoleRequestDto>>(roles);
            return Ok(roleDtos);
        }
        //Get api/role/roleId
        [HttpGet("{RoleId}")]
        public async Task<IActionResult> GetRoleById(Guid RoleId)
        {
            try
            {
                var roles = await _roleService.GetRoleById(RoleId);
                var roleDtos = _mapper.Map<RoleRequestDto>(roles);
                return Ok(roleDtos);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }



        }
        //Post api/role/create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole(CreateRoleRequestDtos request)
        {
            try
            {
                await _roleService.CreateRole(request);
                return Ok();
            }catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //Put api/role/roleId
        [HttpPut("{RoleId}")]
        public async Task<IActionResult> UpdateRole(Guid RoleId, CreateRoleRequestDtos model)
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
        public async Task<IActionResult> FindRole(string Name)
        {
                return Ok(await _roleService.FindRole(Name)); 
        }
        //Post api/role/addusertorole
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AddToRoleModel model)
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
        public async Task<IActionResult> RemoveUserRole(AddToRoleModel model)
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
