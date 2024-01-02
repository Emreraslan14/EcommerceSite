using Emreraslan.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Emreraslan.Services.Concrete
{
    public class DataSeedingService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public DataSeedingService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "Vendor", "Member" };
            if (roles.Length > 0)
            {
                foreach (var roleName in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        var newRole = new Role { Name = roleName };
                        await _roleManager.CreateAsync(newRole);
                    }
                }
            }
        }

        public async Task SeedAdminUserAsync()
        {
            string email = "admin@admin.com";
            string password = "Test1234.";
            string address = "AdminAdress";
            string name = "Admin";
            string surname = "Admin";

            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var user = new User();
                user.Name = name;
                user.UserName = email;
                user.Email = email;
                user.Address = address;
                user.Surname = surname;
                user.EmailConfirmed = true;

                await _userManager.CreateAsync(user, password);

                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
