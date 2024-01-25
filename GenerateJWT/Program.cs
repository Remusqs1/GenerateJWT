using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GenerateJWT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GenerateJwtWithFixedClaims("your-secret-key-1234", "your-issuer", "your-audience", "sub-value-1^n", "jti-value-1", 1626300000));
        }

        public static string GenerateJwtWithFixedClaims(string secret, string issuer, string audience, string sub, string jti, long iat)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            Array.Resize(ref keyBytes, 32);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var myClaims = new List<Claim>
        {
            new Claim("sub", sub),
            new Claim("jti", jti),
            new Claim("iat", iat.ToString(), ClaimValueTypes.Integer) 
        };

            var identity = new ClaimsIdentity(myClaims, "custom", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("issuer", issuer));
            identity.AddClaim(new Claim("audience", audience));  

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.MaxValue, //No exp date
                SigningCredentials = signingCredentials
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(securityTokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}
