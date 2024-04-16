using AutoMapper;
using Infra.Data.Context.Context;
using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using LojaServices_Application.JwtTokenGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LojaServices_Application.Repositories
{
    public class UsuarioRepository : IUsuario
    {

        private readonly ContextDb db;
        private readonly IMapper mapper;
        private readonly IToken tokenGenerator;
        public UsuarioRepository(ContextDb db, IMapper mapper, IToken tokenGenerator)
        {
            this.db = db;
            this.mapper = mapper;
            this.tokenGenerator = tokenGenerator;
        }

        public string LoginUser(Usuario usuario)
        {
            var Token = tokenGenerator.GenerateToken(usuario);
            return Token;  
            
        }


        //Metodos para realizar cadastro de novos usuarios.
        public async Task NewAccount(UsuarioDto usuario)
        {
            var Conta = new ContaUsuario()
            {
                Saldo = 10000,
                RankUser ="Iniciante",
                IDUsuario= usuario.idUsuario
            };
            
            await db.AddAsync(Conta);
            await db.SaveChangesAsync();
        }

        public async Task<UsuarioDto> SingUpUSer(Usuario usuario)
        {
            usuario.IsAdmin = false;
            await db.AddAsync(usuario);
            await db.SaveChangesAsync();

            return mapper.Map<UsuarioDto>(usuario);
        }
    }
}
