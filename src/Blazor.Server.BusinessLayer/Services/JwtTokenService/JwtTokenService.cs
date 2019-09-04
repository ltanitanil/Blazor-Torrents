using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Blazor.Server.BusinessLayer.Settings;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blazor.Server.BusinessLayer.Services.JwtTokenService
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly TokenManagerSettings _tokenSettings;

        public JwtTokenService(IOptions<TokenManagerSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings?.Value;
        }
        public string BuildToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,
                    user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid()
                        .ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.DateOfBirth,user.DateOfBirth.ToShortDateString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(_tokenSettings.ExpiryInDays);

            var token = new JwtSecurityToken(
                _tokenSettings.Issuer,
                _tokenSettings.Audience,
                claims,
                expires: expiry,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
