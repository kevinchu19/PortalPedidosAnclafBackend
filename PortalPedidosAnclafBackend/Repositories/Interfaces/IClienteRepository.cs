using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IClienteRepository: IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetByTermino(string termino, int skip, int take);
    }
}
