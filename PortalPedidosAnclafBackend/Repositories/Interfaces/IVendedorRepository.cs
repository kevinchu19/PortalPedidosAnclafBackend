using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IVendedorRepository : IRepository<Vendedores>
    {
        Task<Vendedores> Get(string id);
    }
}
