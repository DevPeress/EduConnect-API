using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EduConnect.Application.Services;

public class JWTService(IConfiguration config)
{
    private readonly IConfiguration _config = config;
    public string GenerateToken(int userId, string role, bool? lembrar)
    {
        var claims = new[]
        {
            new Claim("id", userId.ToString()),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tempo = lembrar != null && lembrar == true ? 9 : 1;

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(tempo),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
