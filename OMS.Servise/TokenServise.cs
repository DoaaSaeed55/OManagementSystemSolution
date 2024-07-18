using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OMS.Core.Entities;
using OMS.Core.Services.Interfaces;

namespace OMS.Servise
{
    public class TokenServise : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenServise(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(User user, UserManager<User> _userManager)
        {
            //Header

            //Payload
            //privateClaims

            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            };
            //Signature
            //1.key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssues"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
