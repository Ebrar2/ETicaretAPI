using ETicaretAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int minute)
        {
            Application.DTOs.Token token = new Application.DTOs.Token();
            token.Expiration = DateTime.UtcNow.AddMinutes(minute);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                audience: configuration["Token:Audience"],
                issuer: configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials


                );
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken= tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
