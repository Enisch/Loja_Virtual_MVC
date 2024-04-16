using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IUsuario
    {
        Task<UsuarioDto> SingUpUSer(Usuario usuario);
        string LoginUser(Usuario usuario);
        Task NewAccount(UsuarioDto usuario);


    }
}
