﻿using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IListaDePrecioRepository : IRepository<Listasdeprecio>
    {
        Task<Listasdeprecio> Get(string idLista, string idProducto, DateTime fecha);
    }
}
