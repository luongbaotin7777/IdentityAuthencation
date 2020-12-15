﻿using IdentityAuthencation.Entities;
using IdentityAuthencation.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Repository
{
    public interface IUserRepository : IRepositoryBase<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAllUser();
        Task<ApplicationUser> GetUserById(Guid UserId);
    }
}
