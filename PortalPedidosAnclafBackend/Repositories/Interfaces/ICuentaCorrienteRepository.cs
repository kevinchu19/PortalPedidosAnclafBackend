using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface ICuentaCorrienteRepository : IRepository<CuentaCorriente>
    {
        Task<ICollection<CuentaCorriente>> GetByClienteAsync(string cliente);
        Task<ICollection<CuentaCorriente>> GetPendientesByClienteAsync(string cliente);
    }
}
