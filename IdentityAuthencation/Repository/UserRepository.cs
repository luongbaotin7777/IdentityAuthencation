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

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        public ApplicationUser FindByName(string userName)
        {
            return  _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefault(x => x.UserName == userName);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.UserName == userName);
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
