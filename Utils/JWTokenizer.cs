using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using contactManagerAPI.Entities;

namespace contactManagerAPI.Utils
{
    /// <summary>
    /// Utility class for generating JWT tokens.
    /// </summary>
    public class JWTokenizer
    {
        /// <summary>
        /// Generates an access token for the given user using JWT.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <param name="configuration">The configuration containing JWT secret key.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public static string GenerateToken(User user, IConfiguration configuration)
        {
            List<Claim> claims =
                new()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress)
                };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:Key").Value!)
            );

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential,
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"]
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
