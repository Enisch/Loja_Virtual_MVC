using Infra.Data.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Infra.Data.Domain.Interfaces;

namespace LojaServices_Application.JwtTokenGenerator
{
    public class TokenGenerator:IToken
    {
        private readonly IConfiguration configuration;// Será necessario para acessar a secret Key;
        public TokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateToken(Usuario usuario)
        {
            var Token_Claims = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim("Nome", usuario.NomeUsuario!),
                new Claim("Id", usuario.idUsuario.ToString()),
                new Claim("IsAdmin", usuario.IsAdmin.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),

                Issuer = configuration["JWT_Token:Issuer"],
                Audience = configuration["JWT_Token:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["JWT_Token:SecretKey"]!)),SecurityAlgorithms.HmacSha512Signature),
                Expires = DateTime.UtcNow.AddMinutes(10)
            };
                var Handler = new JwtSecurityTokenHandler();  
                var CreatedToken = Handler.CreateJwtSecurityToken(Token_Claims);
                var Token = Handler.WriteToken(CreatedToken);
             
            return Token;
        }
    }
}
