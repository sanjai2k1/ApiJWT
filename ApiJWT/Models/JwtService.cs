﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiJWT.Models
{
    public class JwtService
    {
        private string secureKey = "this is a very secure key jefshfiuoaghmdsnmgnnnnnnnnnnnnnnnnnnnnnnasonoaj[ioawjo[r[jrioe[qjerqjigsperjlgndfgfndlknbcbmnmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmbnkmncoibvnfonoqan[[fngkbnclcm,ccccccccccccccccccccccc";
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));

            var credentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);


            var payload = new JwtPayload(id.ToString(),null,null,null,DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header,payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


        public JwtSecurityToken Verify(String jwt)
        {
            var tokenhandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secureKey);

            tokenhandler.ValidateToken(jwt,new TokenValidationParameters {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            },out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
