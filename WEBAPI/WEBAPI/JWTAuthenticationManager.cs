using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI
{
    public interface IJWTAuthenticationManager
    {
       string Authenticate(string username, string password);
        
    }

    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "Aravind", "Aravind" },
            { "Micro Focus", "Micro Focus" }
        };
         
        private readonly string tokenKey;
       

        public JWTAuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
            
        } 

        public string Authenticate(string username, string password)
        {
            if (!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }

            var token = GenerateTokenString(username, DateTime.UtcNow);
            return token;
        }

        string GenerateTokenString(string username, DateTime expires, Claim[] claims = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(

                 claims ?? new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                //Expire token in minutes
                Expires = expires.AddMinutes(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
