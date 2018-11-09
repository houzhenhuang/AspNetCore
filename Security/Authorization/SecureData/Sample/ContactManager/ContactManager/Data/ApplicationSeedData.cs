using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ContactManager.Models;
using Microsoft.AspNetCore.Identity;
using ContactManager.Authorization;

namespace ContactManager.Data
{
    public static class ApplicationSeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string userPassword)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminId = await EnsureUser(serviceProvider, "admin@contoso.com", userPassword);
                await EnsureRole(serviceProvider, adminId, Constants.ContactAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var userId = await EnsureUser(serviceProvider, "manager@contoso.com", userPassword);
                await EnsureRole(serviceProvider, userId, Constants.ContactManagersRole);

                await SeedDB(context, adminId);
            }
        }

        #region Private Method
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string userName, string userPassword)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                user = new ApplicationUser { UserName = userName };
                await userManager.CreateAsync(user);
            }
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string userId, string roleName)
        {
            IdentityResult result = null;
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                result = await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(userId);

            result = await userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        private static async Task SeedDB(ApplicationDbContext context, string adminId)
        {
            if (context.Contacts.Any())
            {
                return;
            }
            await context.Contacts.AddRangeAsync(
               new Contact
               {
                   Name = "Debra Garcia",
                   Address = "1234 Main St",
                   City = "Redmond",
                   State = "WA",
                   Zip = "10999",
                   Email = "debra@example.com",
                   Status = ContactStatus.Approved,
                   OwnerID = adminId
               },
               new Contact
               {
                   Name = "Thorsten Weinrich",
                   Address = "5678 1st Ave W",
                   City = "Redmond",
                   State = "WA",
                   Zip = "10999",
                   Email = "thorsten@example.com",
                   Status = ContactStatus.Approved,
                   OwnerID = adminId
               },
               new Contact
               {
                   Name = "Yuhong Li",
                   Address = "9012 State st",
                   City = "Redmond",
                   State = "WA",
                   Zip = "10999",
                   Email = "yuhong@example.com",
                   Status = ContactStatus.Approved,
                   OwnerID = adminId
               },
               new Contact
               {
                   Name = "Jon Orton",
                   Address = "3456 Maple St",
                   City = "Redmond",
                   State = "WA",
                   Zip = "10999",
                   Email = "jon@example.com",
                   OwnerID = adminId
               },
               new Contact
               {
                   Name = "Diliana Alexieva-Bosseva",
                   Address = "7890 2nd Ave E",
                   City = "Redmond",
                   State = "WA",
                   Zip = "10999",
                   Email = "diliana@example.com",
                   OwnerID = adminId
               });
            await context.SaveChangesAsync();
        }
        #endregion
    }
}
