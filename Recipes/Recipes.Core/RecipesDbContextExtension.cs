using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Core
{
    public static class RecipesDbContextExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();
            string MANAGER_ROLE_ID = Guid.NewGuid().ToString();
            string USER_ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                     Id = MANAGER_ROLE_ID,
                     Name = "Manager",
                     NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = USER_ROLE_ID,
                    Name = "User",
                    NormalizedName = "USER"
                });

            string ADMIN_ID = Guid.NewGuid().ToString();
            string MANAGER_ID = Guid.NewGuid().ToString();
            string USER_ID = Guid.NewGuid().ToString();

            var admin = new User
            {
                Id = ADMIN_ID,
                UserName = "admin@recipes.com",
                Email = "admin@recipes.com",
                EmailConfirmed = true,
                NormalizedEmail = "admin@recipes.com".ToUpper(),
                NormalizedUserName = "admin@recipes.com".ToUpper()
            };
            var manager = new User
            {
                Id = MANAGER_ID,
                UserName = "manager@recipes.com",
                Email = "manager@recipes.com",
                EmailConfirmed = true,
                NormalizedEmail = "manager@recipes.com".ToUpper(),
                NormalizedUserName = "manager@recipes.com".ToUpper()
            };
            var user = new User
            {
                Id = USER_ID,
                UserName = "user@recipes.com",
                Email = "user@recipes.com",
                EmailConfirmed = true,
                NormalizedEmail = "user@recipes.com".ToUpper(),
                NormalizedUserName = "user@recipes.com".ToUpper()
            };

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            admin.PasswordHash = hasher.HashPassword(admin, "admin$Pass1");
            manager.PasswordHash = hasher.HashPassword(manager, "manager$Pass1");
            user.PasswordHash = hasher.HashPassword(user, "user$Pass1");

            builder.Entity<User>().HasData(admin, manager, user);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                     RoleId = MANAGER_ROLE_ID,
                     UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                      RoleId = USER_ROLE_ID,
                      UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = MANAGER_ROLE_ID,
                    UserId = MANAGER_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = USER_ROLE_ID,
                    UserId = MANAGER_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = USER_ROLE_ID,
                    UserId = USER_ID
                });
            
           
        }
    }
}
