using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IUsuarioDtoMethods
    {
        Task<string> Login(LogginUsuario logginUsuario);
        Task<bool> CheckAvailbilityOfEmail(string email);
        Task<UsuarioDto> cadastroDeUsuario(UsuarioDto usuarioDto);
        Task<bool> CheckAvailbilityOfName(string name);
        Task AuthenticateUser(string Token);
        
    }
}
