using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Api.UserModels;
using Microsoft.IdentityModel.Tokens;

namespace backend.Api.JwtService;

public class Jwt_Service
{
    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Username", user.Username!),
            new Claim("Email", user.Email!)
        };
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(24),
            claims: claims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECRET_KEY_TESTSECRET_KEY_TESTSECRET_KEY_TESTSECRET_KEY_TESTSECRET_KEY_TEST")),
            SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}