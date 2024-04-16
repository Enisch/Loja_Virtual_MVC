﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IPasswordProtected
    {
        byte[] PasswordHashed(string password);
        bool VerifyPassword(string Inputpassword, byte[] PasswordFromUser);

    }
}
