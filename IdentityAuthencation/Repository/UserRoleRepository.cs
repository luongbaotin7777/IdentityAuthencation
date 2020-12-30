using IdentityAuthencation.Entities;
using IdentityAuthencation.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Repository
{
    public class UserRoleRepository : RepositoryBase<ApplicationUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(RepositoryDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
