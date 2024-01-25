using JwtLogin.Core.Contexts.AccountContext.Entities;
using JwtLogin.Core;
using JwtLogin.Core.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace JwtLogin.Api.Extensions
{
    public static class JwtExtension
    {
        public static string Generate(ResponseData data)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(8),
                Subject = GenerateClaims(data)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(ResponseData user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("Id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }
    }
}
