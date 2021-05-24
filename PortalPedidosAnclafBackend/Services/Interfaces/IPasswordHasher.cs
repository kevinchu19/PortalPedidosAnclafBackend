﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Check(string hash, string password);
    }
}
