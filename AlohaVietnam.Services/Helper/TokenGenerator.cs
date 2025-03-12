﻿using AlohaVietnam.Repositories.Models;
using AlohaVietnam.Services.Interfaces;
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

namespace AlohaVietnam.Services.Helper
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateJwtToken(User user, string? name, string role)
        {
            var date = DateTime.UtcNow;
            TimeZoneInfo asianZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            date = TimeZoneInfo.ConvertTimeFromUtc(date, asianZone);
            Console.WriteLine(date);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName.ToString()),
                new Claim("role", role),
                new Claim("fullName", name ??= user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtToken:Issuer"],
                audience: _configuration["JwtToken:Audience"],
                claims: claims,
                expires: date.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public Task<string> GenerateRefreshToken() => Task.FromResult(Guid.NewGuid().ToString());

    }
}

