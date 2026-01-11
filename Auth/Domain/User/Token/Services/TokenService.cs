using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.User.Token.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Domain.User.Token.Services
{
    public class TokenService : ITokenService
    {
        private const int _HOURS_TO_EXPIRE_TOKEN = 2;

        public AccessToken GenerateToken(Auth.Domain.User.Models.User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Role))
                throw new InvalidOperationException("User name and role are required to generate a token.");

            var tokenHandler = new JwtSecurityTokenHandler();
            // TODO: Move secret to a secure configuration
            var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");
            var tokenSpecificationDescriptor = DescribeTokenSpecification(user, key);
            var securityToken = tokenHandler.CreateToken(tokenSpecificationDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return new AccessToken(token, user.Username, user.Role);
        }

        private SecurityTokenDescriptor DescribeTokenSpecification(Auth.Domain.User.Models.User user, byte[] key)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = ConfigureClaimsIdentity(user),
                Expires = DateTime.UtcNow.AddHours(_HOURS_TO_EXPIRE_TOKEN),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenDescriptor;
        }

        private ClaimsIdentity ConfigureClaimsIdentity(Auth.Domain.User.Models.User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Role))
                throw new InvalidOperationException("User name and role are required to generate a token.");
                
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                    });
            return claimsIdentity;
        }
    }
}