using AutoMapper;
using Infra.Data.Context.Context;
using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LojaServices_Application.Repositories
{
    public class UsuarioDtoMethodsRepository : IUsuarioDtoMethods
    {
        private IUsuario usuarioMethods;
        private readonly IPasswordProtected passwordProtected;
        private readonly IMapper mapper;
        private readonly ContextDb db;

        public UsuarioDtoMethodsRepository(IUsuario usuarioMethods, IPasswordProtected passwordProtected, IMapper mapper, ContextDb db)
        {
            this.usuarioMethods = usuarioMethods;
            this.passwordProtected = passwordProtected;
            this.mapper = mapper;
            this.db = db;
        }

        public async Task<UsuarioDto> cadastroDeUsuario(UsuarioDto usuarioDto)
        {

            var HashSenha =  passwordProtected.PasswordHashed(usuarioDto.Password);

            var NewUser = mapper.Map<Usuario>(usuarioDto);
            NewUser.Senha = HashSenha;
            
            var User = await usuarioMethods.SingUpUSer(NewUser);
               
            await usuarioMethods.NewAccount(User!);

            return User;
        }
        //Verify if User's exist before registration
        public async Task<bool> CheckAvailbilityOfEmail(string email)
        {
            
            var UserEmail = await  db.usuarios.Where(x=> x.EmailUsuario!.ToLower() == email.ToLower()).FirstOrDefaultAsync();

            if(UserEmail == null)
            {
                return true;// The Email's Availble to Register;
            }

            return false;//The Email's not Availble to Register;Caso seja falso o E-mail do usuario já é utilizado;
        }

        public async Task<bool> CheckAvailbilityOfName(string name)
        {
            var UserName = await db.usuarios.Where(x=> x.NomeUsuario!.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if (UserName == null)
                return true;

            return false;//False means the UserName isn't avialble to Use; Caso seja falso o Nome de usuario já é utilizado;
            
        }

        public async Task<string> Login(LogginUsuario logginUsuario)
        {
            //Esses dois metodos checam a legitimidade da senha e email do usuario
            if(await CheckAvailbilityOfEmail(logginUsuario.EmailUser!))
            {
                throw new Exception("Este E-mail não existe, digite o endereço de email corretamente ou realize o cadastro.");
               
            }
            var User=await db.usuarios.Where(x=> x.EmailUsuario == logginUsuario.EmailUser).FirstOrDefaultAsync();
            if(!passwordProtected.VerifyPassword(logginUsuario.PasswordUser!,User!.Senha!))
            {
                throw new Exception("Senha Invalida.\nTente outra senha.");
            }
            //Feito as checagens é gerado o Token
            var Token=usuarioMethods.LoginUser(User);
            return Token;
        }

        //Permite o usuario acessar alguns controllers que necessitam de autorização
        //Metodo ainda em teste, encontrei diversas dificuldades em realizar a autorização.
        public Task AuthenticateUser(string Token)
        {
            throw new NotImplementedException();

        }
    }
}
