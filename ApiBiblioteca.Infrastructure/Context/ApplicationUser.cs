using Microsoft.AspNetCore.Identity;

namespace ApiBiblioteca.Infrastructure.Context;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}
