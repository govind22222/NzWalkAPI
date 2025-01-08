using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using NZWalkAPI.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalkAPI.Repository
{
    public class AuthService : IAuth
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        #region Creating JWT Token
        public string CreateJwtToken(IdentityUser identityUser, List<string> roles)
        {
            // Creating list of claim and adding email, role claim.
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));
            foreach (var roleVal in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleVal));
            }

            // 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                signingCredentials: cred,
                expires: DateAndTime.Now.AddMinutes(60)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
