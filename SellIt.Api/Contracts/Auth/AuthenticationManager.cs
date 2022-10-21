﻿namespace SellIt.Api.Contracts.Auth
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using SellIt.Api.Models;
    using SellIt.Api.Services.Auth;
    using SellIt.Infrastructure.Data.Models;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthenticationManager : IAuthenticationManager
    {

        private readonly UserManager<User> usermanager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthenticationManager(UserManager<User> usermanager, IConfiguration configuration)
        {
            this.usermanager = usermanager;
            _configuration = configuration;
        }
        public async Task<bool> ValidateCredentials(AuthCredentials credentials)
        {
             _user = await usermanager.FindByNameAsync(credentials.Username);
            return _user != null && await usermanager.CheckPasswordAsync(_user, credentials.Password);
        }

        public async Task<string> CreateToken()
        {
            //List<Claim> claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, _user.UserName),
            //    new Claim(ClaimTypes.Role, "Administrator")
            //};

            var claims = new List<Claim>
            { new Claim(ClaimTypes.Name, _user.UserName) };

            var roles = await usermanager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }   
    }
}
