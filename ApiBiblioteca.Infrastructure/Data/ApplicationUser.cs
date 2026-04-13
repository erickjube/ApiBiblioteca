using Microsoft.AspNetCore.Identity;

namespace ApiBiblioteca.Infrastructure.Data;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}
