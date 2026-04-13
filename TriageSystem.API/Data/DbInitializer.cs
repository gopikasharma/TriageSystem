
using Microsoft.AspNetCore.Identity;
using TriageSystem.API.Entities;
using TriageSystem.API.Shared;

namespace TriageSystem.API.Data;

public static class DbInitializer
{
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        string[] roleNames = { "Admin", "Doctor", "Patient" ,"Nurse", "Receptionist", "Reviewer"};
        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if(!roleExists)
            {
                await roleManager.CreateAsync(new AppRole {Name = roleName});
            }
        }
        var adminEmail = "admin@abchospital.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if(adminUser == null)
        {
            var newAdmin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Jane",
                LastName = "Doe",
                PhoneNumber = "0000000000",
                Sex = BiologicalSex.Female
            };
            var createAdmin = await userManager.CreateAsync(newAdmin, "Admin@123!");
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
        }
    }   
}
