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
            CreateMap<ApplicationRole, RoleRequestDto>()
             .ForMember(destination => destination.RoleName, options => options.MapFrom(source => source.Name));
            CreateMap<RoleRequestDto, ApplicationRole>()
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.RoleName));

            CreateMap<CreateRoleRequestDtos, ApplicationRole>()
                 .ForMember(destination => destination.Name, options => options.MapFrom(source => source.RoleName)); ;
            CreateMap<ApplicationRole, CreateRoleRequestDtos>()
                 .ForMember(destination => destination.RoleName, options => options.MapFrom(source => source.Name)); ;

            //User
            CreateMap<ApplicationUser, RegisterRequestDto>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.UserName))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNumber));

            CreateMap<RegisterRequestDto, ApplicationUser>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.UserName))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNumber));

            CreateMap<UserDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>();

            CreateMap<UpdateUserRequestDto, ApplicationUser>();
            CreateMap<ApplicationUser, UpdateUserRequestDto>();
        }
    }
}
