﻿// <auto-generated />
using System;
using IdentityAuthencation.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityAuthencation.Migrations
{
    [DbContext(typeof(RepositoryDbContext))]
    [Migration("20201229023332_add-password")]
    partial class addpassword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AppRoleClaims");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.View",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.Create",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.Edit",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.Delete",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 5,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.View",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 6,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Create",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 7,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Edit",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 8,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Delete",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 9,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.View",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 10,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Create",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 11,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Edit",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 12,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Delete",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 40,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.View",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 41,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.Create",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 42,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.Edit",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 43,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.Delete",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 13,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Dashboards.View",
                            RoleId = new Guid("d7269987-becd-43c8-a516-4316c6aa30d2")
                        },
                        new
                        {
                            Id = 14,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.View",
                            RoleId = new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f")
                        },
                        new
                        {
                            Id = 15,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.View",
                            RoleId = new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f")
                        },
                        new
                        {
                            Id = 16,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Dashboards.View",
                            RoleId = new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f")
                        },
                        new
                        {
                            Id = 17,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.View",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 18,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.Create",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 19,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.Edit",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 20,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.View",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 21,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Create",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 22,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Edit",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 23,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Delete",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 24,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.View",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 25,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Create",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 26,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Edit",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 27,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Delete",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 28,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Dashboards.View",
                            RoleId = new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152")
                        },
                        new
                        {
                            Id = 29,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Dashboards.View",
                            RoleId = new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f")
                        },
                        new
                        {
                            Id = 30,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.View",
                            RoleId = new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f")
                        },
                        new
                        {
                            Id = 31,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Create",
                            RoleId = new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f")
                        },
                        new
                        {
                            Id = 32,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Edit",
                            RoleId = new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f")
                        },
                        new
                        {
                            Id = 33,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.View",
                            RoleId = new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f")
                        },
                        new
                        {
                            Id = 34,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Create",
                            RoleId = new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f")
                        },
                        new
                        {
                            Id = 35,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Edit",
                            RoleId = new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f")
                        },
                        new
                        {
                            Id = 36,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.View",
                            RoleId = new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4")
                        },
                        new
                        {
                            Id = 37,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Products.Create",
                            RoleId = new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4")
                        },
                        new
                        {
                            Id = 38,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.View",
                            RoleId = new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4")
                        },
                        new
                        {
                            Id = 39,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Categories.Create",
                            RoleId = new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4")
                        });
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUserRoles");
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationRoleClaim", b =>
                {
                    b.HasOne("IdentityAuthencation.Entities.ApplicationRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUser", b =>
                {
                    b.OwnsMany("IdentityAuthencation.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("AccessToken")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("ApplicationUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Token")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("ApplicationUserId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");
                        });
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserClaim", b =>
                {
                    b.HasOne("IdentityAuthencation.Entities.ApplicationUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserLogin", b =>
                {
                    b.HasOne("IdentityAuthencation.Entities.ApplicationUser", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityAuthencation.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserRole", b =>
                {
                    b.HasOne("IdentityAuthencation.Entities.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityAuthencation.Entities.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityAuthencation.Entities.ApplicationUserToken", b =>
                {
                    b.HasOne("IdentityAuthencation.Entities.ApplicationUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
