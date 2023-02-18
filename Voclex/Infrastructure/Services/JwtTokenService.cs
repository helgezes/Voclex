﻿using Application.ModelInterfaces.DtoInterfaces;
using Application.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public sealed class JwtTokenService : IAuthTokenService
    {
        private const int expirationOffsetInMinutes = 24 * 60;

        public string CreateToken(IUserDto user)
        {
            var token = GetJwtSecurityToken(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }


        private static JwtSecurityToken GetJwtSecurityToken(IUserDto user)
        {
            return new JwtSecurityToken(
                claims: GetClaims(user),
                expires: GetExpiration(),
                signingCredentials: GetSigningCredentials());
        }

        private static SigningCredentials GetSigningCredentials()
        {
            return new SigningCredentials(
                GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
        }

        private static DateTime GetExpiration()
        {
            return DateTime.UtcNow.AddMinutes(expirationOffsetInMinutes);
        }

        private static Claim[] GetClaims(IUserDto user)
        {
            return new[] { new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name) };
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey("!SomethingSecret!"u8.ToArray());
        }
    }
}