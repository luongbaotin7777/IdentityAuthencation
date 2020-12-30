using IdentityAuthencation.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryDbContext _context;
        private IUserRepository _user;
        private IRoleRepository _role;
        private IUserRoleRepository _userRole;
        private IUserClaimRepository _userClaim;
        public UnitOfWork(RepositoryDbContext context)
        {
            _context = context;
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_context);
                }
                return _role;
            }
        }

        public IUserRoleRepository UserRole
        {
            get
            {
                if (_userRole == null)
                {
                    _userRole = new UserRoleRepository(_context);
                }
                return _userRole;
            }
        }

        public IUserClaimRepository UserClaim
        {
            get
            {
                if (_userClaim == null)
                {
                    _userClaim = new UserClaimRepository(_context);
                }
                return _userClaim;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();

        }
    }
}
