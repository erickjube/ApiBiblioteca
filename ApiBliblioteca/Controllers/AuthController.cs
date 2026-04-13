using ApiBiblioteca.Application.DTOs.DtosAuth;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiBiblioteca.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _userManager.FindByNameAsync(login.Usuario!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, login.Senha!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegistroDto registro)
    {
        var userExists = await _userManager.FindByNameAsync(registro.Usuario!);
        if (userExists != null) return BadRequest("Usuario já existe!");
        ApplicationUser user = new()
        {
            Email = registro.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registro.Usuario
        };
        var result = await _userManager.CreateAsync(user, registro.Senha!);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok("Usuario criado com sucesso!");
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenDto token)
    {
        if (token is null) return BadRequest("Request invalido!");
        
        string? AccessToken = token.AccessToken ?? throw new ArgumentNullException(nameof(token));
        string? refreshToken = token.RefreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
        var principal = _tokenService.GetPrincipalFromExpiredToken(AccessToken!, _configuration);

        if (principal is null) return BadRequest("Access Token/Refresh Token invalido!");
        
        string username = principal.Identity.Name;
        var user = await _userManager.FindByNameAsync(username!);
        
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpireTime <= DateTime.UtcNow)
            return BadRequest("Access Token/Refresh Token invalido!");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("revoke/{usuario}")]
    public async Task<IActionResult> Revoke(string usuario)
    {
        var user = await _userManager.FindByNameAsync(usuario);
        if (user is null) return BadRequest("Name do usuario invalido!");
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("create-role")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (roleResult.Succeeded) return Ok($"Role {roleName} adicionada com sucesso!");
            return BadRequest($"Erro adicionando a role {roleName}!");
        }
        return BadRequest("Role já existe!");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("add-user-to-role")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null) 
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded) return Ok($"User {user.Email} adicionado a role {roleName} com sucesso!");
            return BadRequest($"Erro ao adicionar {user.Email} a role {roleName}");
        }
        return BadRequest("Usuario não encontrado!");
    }
}
