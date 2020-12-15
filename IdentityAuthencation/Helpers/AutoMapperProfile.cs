using AutoMapper;
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Role
            CreateMap<ApplicationRole, RoleRequestDto>();
            CreateMap<RoleRequestDto, ApplicationRole>();

            CreateMap<CreateRoleRequestDto, ApplicationRole>();
            CreateMap<ApplicationRole, CreateRoleRequestDto>();
            //User
            CreateMap<ApplicationUser, RegisterRequestDto>();
            CreateMap<RegisterRequestDto, ApplicationUser>();

            CreateMap<RegisterDto, ApplicationUser>();

            CreateMap<UserDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dto => dto.roles, opt => opt.MapFrom(x => x.UserRoles.Select(y => y.Role).ToList()));

            CreateMap<UpdateUserRequestDto, ApplicationUser>();
            CreateMap<ApplicationUser, UpdateUserRequestDto>();
        }
    }
}
