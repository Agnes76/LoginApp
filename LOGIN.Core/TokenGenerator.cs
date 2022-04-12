using LOGIN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LOGIN.Core
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        public TokenGenerator(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(AppUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingKey = new SymmetricSecurityKey
                                  (Encoding.UTF8.GetBytes(_config["JWTSettings:SecretKey"]));

            var token = new JwtSecurityToken
                (audience: _config["JWTSettings:Audience"],
                  issuer: _config["JWTSettings:Issuer"],
                  claims: authClaims,
                  expires: DateTime.UtcNow.AddMinutes(10),
                  signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                 );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
