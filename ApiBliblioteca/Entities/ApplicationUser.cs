using Microsoft.AspNetCore.Identity;

namespace ApiBiblioteca.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}
