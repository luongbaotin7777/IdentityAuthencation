using IdentityAuthencation.Entities;
using IdentityAuthencation.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Repository
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository(RepositoryDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUser()
        {
            return await _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).ToListAsync();
        }
        public async Task<ApplicationUser> GetUserById(Guid UserId)
        {
            return await _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Id == UserId);
        }
    }
}
