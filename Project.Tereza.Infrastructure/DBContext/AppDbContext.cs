using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Tereza.Core;
using Project.Tereza.Core.Entities.Identity;

namespace Project.Tereza.Infrastructure.DBContext
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid,
        IdentityUserClaim<Guid>, AppUserRoles, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<Need>? Needs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // seed needs
            SeedNeeds(modelBuilder);

            // seed roles
            SeedRoles(modelBuilder);

            // seed users
            SeedUsers(modelBuilder);

            // seed user role relations
            SeedUserRoleRelations(modelBuilder);
        }

        private static void SeedNeeds(ModelBuilder modelBuilder)
        {
            var needs = new List<Need>
            {
                new("82d257a5-d72b-4f08-bcf2-76ebdc958b5f", "Laptop", "Need laptop for working needs.", 1, false),
                new("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82", "Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
                new("a961067e-c777-42c2-8fee-71180d750bd7", "Aspirin", "Please, I can't find this drug in retail", 3, false)
            };

            foreach (var need in needs)
            {
                modelBuilder.Entity<Need>().HasData(need);
            }
        }
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>()
                .ToTable("AppRoles")
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            IEnumerable<AppRole> roles = new List<AppRole>
            {
                new AppRole
                {
                    Id = Guid.Parse("0a34f5d9-2211-42ef-80a0-65d9f24dc67e"),
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = "0a34f5d9-2211-42ef-80a0-65d9f24dc67e",
                    RoleDescription = "May manage Admins, Highest permissions"
                },
                new AppRole
                {
                    Id = Guid.Parse("53afebc1-3d10-4b5f-a8c7-a4f4a8628bc1"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "53afebc1-3d10-4b5f-a8c7-a4f4a8628bc1",
                    RoleDescription = "Ordinary Admin"
                },
                new AppRole
                {
                    Id = Guid.Parse("7faec745-6e30-4428-ba6a-7f1140146ff2"),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "7faec745-6e30-4428-ba6a-7f1140146ff2",
                    RoleDescription = "Tipical user"
                },
            };

            modelBuilder.Entity<AppRole>().HasData(roles);
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .ToTable("AppUsers")
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            IEnumerable<AppUser> users = new List<AppUser>
            {
                new AppUser
                {
                    Id = Guid.Parse("5bb520a6-02bc-4dc0-b805-e5c66783898a"),
                    Name = "SuperAdmin",
                    Surname = "SuperAdmin",
                    Email = "superadmin@projecttereza.org",
                    EmailConfirmed = false,
                    NormalizedEmail = "superadmin@projecttereza.org".ToUpper(),
                    UserName = "SuperAdmin",
                    NormalizedUserName = "SuperAdmin".ToUpper(),
                    PasswordHash = "AQAAAAEAACcQAAAAEGE+LGK3sVpq1x8dp5WUJWQC6Jy/LIzhf2C0h38cJsqiuNkH+boBPUBstf8/t1BvBg==",
                    SecurityStamp = "R2QKAFY6HJCWISKITA7JHISY2B4ISNIZ",
                    ConcurrencyStamp = "5bb520a6-02bc-4dc0-b805-e5c66783898a",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                },
                new AppUser
                {
                    Id = Guid.Parse("f14bb510-b4fb-4e8b-bd99-e5426f55c8f8"),
                    Name = "TestUser",
                    Surname = "TestUser",
                    Email = "testuser@gmail.com",
                    EmailConfirmed = false,
                    NormalizedEmail = "testuser@gmail.com".ToUpper(),
                    UserName = "TestUser",
                    NormalizedUserName = "TestUser".ToUpper(),
                    PasswordHash = "AQAAAAEAACcQAAAAEJj2DDiwxfh3IkBUdzzcLy8oFkjr7uYEFQn8JpdaXPB65GYTJtW9sLrFY6X8/BAL+g==",
                    SecurityStamp = "PULC6TBDHH3ZKN7Q4CNWX4QRPKI7ZQKR",
                    ConcurrencyStamp = "f14bb510-b4fb-4e8b-bd99-e5426f55c8f8",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                }
            };

            modelBuilder.Entity<AppUser>().HasData(users);
        }

        private static void SeedUserRoleRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserRoles>().ToTable("AppUsersRoles");

            modelBuilder.Entity<AppUserRoles>().HasData(new AppUserRoles()
            {
                RoleId = Guid.Parse("0a34f5d9-2211-42ef-80a0-65d9f24dc67e"),
                UserId = Guid.Parse("5bb520a6-02bc-4dc0-b805-e5c66783898a")
            });

            modelBuilder.Entity<AppUserRoles>().HasData(new AppUserRoles
            {
                RoleId = Guid.Parse("7faec745-6e30-4428-ba6a-7f1140146ff2"),
                UserId = Guid.Parse("f14bb510-b4fb-4e8b-bd99-e5426f55c8f8")
            });
        }
    }
}