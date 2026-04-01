using ApiBiblioteca.Entities;
using Microsoft.AspNetCore.Identity;

namespace ApiBiblioteca.Seeders;

// Classe para quando a aplicação iniciar, criar as roles e o usuário admin caso não existam
public static class IdentitySeeder
{
    public static async Task SeedRolesAndAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // criar roles
        if (!await roleManager.RoleExistsAsync("Admin")) await roleManager.CreateAsync(new IdentityRole("Admin"));
        if (!await roleManager.RoleExistsAsync("Bibliotecario")) await roleManager.CreateAsync(new IdentityRole("Bibliotecario"));

        // criar admin
        var adminEmail = "admin@biblioteca.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Admin@123");
            if (result.Succeeded) await userManager.AddToRoleAsync(user, "Admin");
        }

        if (!await userManager.IsInRoleAsync(admin!, "Admin")) await userManager.AddToRoleAsync(admin, "Admin");
    }
}
