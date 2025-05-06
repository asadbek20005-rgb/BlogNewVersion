using Blog.Common.Models.Jwt;
using Blog.Data.Entities;
using Blog.Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Service.Jwt;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private JwtModel Jwt = configuration.GetSection("JwtSettings").Get<JwtModel>()!;
    public string CreateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        };
        var token = new JwtSecurityToken(
            issuer: Jwt.Issuer,
            audience: Jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
