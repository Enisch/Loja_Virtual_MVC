using Infra.Data.Context.Context;
using Infra.Data.Domain.AutoMappers;
using Infra.Data.Domain.Interfaces;
using LojaServices_Application.JwtTokenGenerator;
using LojaServices_Application.PasswordHasher;
using LojaServices_Application.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Ioc.Services_
{
    public static class Services_
    {
        public static IServiceCollection ServicesInJection(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            var StringConnection = configuration.GetConnectionString("DefaultString");
            services.AddDbContext<ContextDb>(x => x.UseMySql(StringConnection, ServerVersion.AutoDetect(StringConnection)));

            //Repositories Injection
            services.AddScoped<IPasswordProtected, PasswordProtectedRepository>();
            services.AddScoped<IProdutosCompras,ProdutosComprasRepository>();
            services.AddScoped<IProdutosFiltros,ProdutosFiltrosRepository>();
            services.AddScoped<IUsuario, UsuarioRepository>();
            services.AddScoped<IUsuarioDtoMethods,UsuarioDtoMethodsRepository>();
            services.AddScoped<IToken,TokenGenerator>();


            //AutoMapper
            services.AddAutoMapper(typeof(MapperUsuario));


            //JWT Configurations

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.RequireAuthenticatedSignIn = true;
                

            }).AddJwtBearer(x=> {
                x.SaveToken = true;
                
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT_Token:Issuer"],
                    ValidAudience = configuration["JWT_Token:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_Token:SecretKey"]!)),

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            
            return services;
        }
    }
}
