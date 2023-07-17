using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using hexagonal.Domain.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace hexagonal.Application.Components.AuthenticationComponent.SecurityCore.UseCases;

public class UcGenerateToken : IUcGenerateToken
{
    private readonly IConfiguration _configuration;

    public UcGenerateToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> Execute(TokenUser request)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>
            {
                new("Key", request.Key.ToString()),
                new(ClaimTypes.Name, request.Name)
            };

            foreach (var role in request.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}