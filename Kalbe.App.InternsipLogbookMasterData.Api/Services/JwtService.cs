﻿
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IJwtService
    {
        string GenerateToken(UserExternal userProfile);
        string GenerateTokenInternal(UserInternal userProfile);
    }
    public class JwtService : IJwtService
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private IConfiguration _configuration;
        private readonly string _secret;

        public JwtService(IOptions<JwtConfiguration> jwtConfiguration, IConfiguration configuration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _configuration = configuration;
            _secret = _configuration.GetSection("AppJwtSecret").Value;
        }

        public string GenerateToken(UserExternal userProfile)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userProfile.GetClaims()),
                Expires = DateTime.UtcNow.AddDays(_jwtConfiguration.ExpirationDay),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfiguration.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateTokenInternal(UserInternal userProfile)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userProfile.GetClaims()),
                Expires = DateTime.UtcNow.AddDays(_jwtConfiguration.ExpirationDay),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfiguration.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
