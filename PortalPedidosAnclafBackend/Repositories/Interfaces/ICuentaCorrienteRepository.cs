using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface ICuentaCorrienteRepository : IRepository<CuentaCorriente>
    {
        Task<ICollection<object>> GetByClienteAsync(string cliente, string idVendedor, string fechaDesde, string fechaHasta);
        Task<ICollection<CuentaCorriente>> GetPendientesByClienteAsync(string cliente, string idVendedor, string fechaDesde, string fechaHasta);
        Task<CuentaCorriente> Get(CuentaCorriente primaryKey);
    }
}
