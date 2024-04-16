using AutoMapper;
using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.AutoMappers
{
    public class MapperUsuario:Profile
    {
        public MapperUsuario()
        {
            CreateMap<Usuario,UsuarioDto>().ReverseMap();
            CreateMap<UsuarioDto, UsuarioDto>();
        }
    }
}
