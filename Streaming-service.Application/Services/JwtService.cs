using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Streaming_service.Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string JwtGenerate(User user)
    {
        var jwtSettings = _config.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}