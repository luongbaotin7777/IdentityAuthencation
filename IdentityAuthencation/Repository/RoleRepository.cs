using IdentityAuthencation.Entities;
using IdentityAuthencation.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Repository
{
    public class RoleRepository : RepositoryBase<ApplicationRole>, IRoleRepository

    {

        public RoleRepository(RepositoryDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

    }
}
