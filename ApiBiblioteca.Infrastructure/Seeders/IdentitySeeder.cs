using ApiBiblioteca.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace ApiBiblioteca.Seeders;

// Classe para quando a aplicação iniciar, criar as roles e o usuário admin caso não existam
public static class IdentitySeeder
{
    public static async Task SeedRolesAndAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // criar role
        if (!await roleManager.RoleExistsAsync("Admin")) await roleManager.CreateAsync(new IdentityRole("Admin"));

        // criar admin
        var admins = await userManager.GetUsersInRoleAsync("Admin");

        if (!admins.Any())
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@biblioteca.com",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Admin@123");
            if (result.Succeeded) await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
