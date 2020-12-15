using IdentityAuthencation.Entities;
using IdentityAuthencation.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Repository.BaseRepository
{
    public class RepositoryDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("AppUsers");
                b.Property(p => p.FirstName).HasMaxLength(80).IsRequired();
                b.Property(p => p.LastName).HasMaxLength(80).IsRequired();

                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();
                b.HasMany(e => e.Logins)
                   .WithOne()
                   .HasForeignKey(ul => ul.UserId)
                   .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("AppRoles");
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();
                b.Property(p => p.NormalizedName).HasMaxLength(50);
                b.Property(p => p.Description).HasMaxLength(50).IsRequired();

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("AppUserRoles");
            });
            modelBuilder.Entity<ApplicationRoleClaim>(b =>
            {
                b.ToTable("AppRoleClaims");
            });
            modelBuilder.Entity<ApplicationUserClaim>(b =>
            {
                b.ToTable("AppUserClaims");
            });
            modelBuilder.Entity<ApplicationUserLogin>(b =>
            {
                b.ToTable("AppUserLogins");
            }); modelBuilder.Entity<ApplicationUserToken>(b =>
             {
                 b.ToTable("AppUserTokens");
             });

            //Seed Data
            modelBuilder.Seed();
        }

    }
}
