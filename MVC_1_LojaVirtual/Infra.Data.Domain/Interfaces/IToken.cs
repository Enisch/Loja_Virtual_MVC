﻿using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IToken
    {
        string GenerateToken(Usuario usuario);
    }
}
