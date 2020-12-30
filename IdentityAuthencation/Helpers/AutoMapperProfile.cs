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
            CreateMap<ApplicationRole, RoleResponseDto>();
            CreateMap<RoleResponseDto, ApplicationRole>();

            CreateMap<RoleRequestDto, ApplicationRole>();
            CreateMap<ApplicationRole, RoleRequestDto>();
            //User
            CreateMap<ApplicationUser, RegisterAdminDto>();
            CreateMap<RegisterAdminDto, ApplicationUser>();

            CreateMap<RegisterDto, ApplicationUser>();

            CreateMap<UserResponseDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserResponseDto>()
                .ForMember(dto => dto.roles, opt => opt.MapFrom(x => x.UserRoles.Select(y => y.Role).ToList()));

            CreateMap<UserRequestDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserRequestDto>();
        }
    }
}
