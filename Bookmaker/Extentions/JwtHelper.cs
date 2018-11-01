using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Bookmaker.Extentions
{
    public class JwtHelper
    {
        public static string GenerateToken(int userId)
        {
            var claims = new[] { new Claim("user_id", userId.ToString(), ClaimValueTypes.Integer) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Scope.SecretKey));
            var signInCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var dateNow = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                audience: Scope.ValidAudience,
                issuer: Scope.ValidIssuer,
                notBefore: dateNow,
                expires: dateNow.Add(TimeSpan.FromHours(Scope.LifeTime)),
                claims: claims,
                signingCredentials: signInCredential
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static int ValidateToken(string token)
        {
            var validationParameters =
                new TokenValidationParameters
                {
                    ValidAudience = Scope.ValidAudience,
                    ValidIssuer = Scope.ValidIssuer,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Scope.SecretKey))
                };
            var claims = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out var validatedToken);
            return int.Parse(claims.Claims.Where(c => c.Type == "user_id").FirstOrDefault().Value);
        }
    }
}
