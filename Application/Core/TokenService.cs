using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Application.Core
{
    public class TokenService
    {
        private readonly ApplicationProperties applicationProperties;

        public TokenService(ApplicationProperties applicationProperties)
        {
            this.applicationProperties = applicationProperties;
        }

        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            // TODO: get from client name from applicationProperties make a strong key and then pass it as parameter
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationProperties.JWTKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // TODO: make add hours to parameter
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
