using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class CuentaCorrienteRepository : RepositoryBase<CuentaCorriente>, ICuentaCorrienteRepository
    {
        public CuentaCorrienteRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }

        public async Task<ICollection<CuentaCorriente>> GetByClienteAsync(string cliente)
        {
            return await Context.Set<CuentaCorriente>().Where(c => c.Idcliente == cliente).ToListAsync();
        }

        public async Task<ICollection<CuentaCorriente>> GetPendientesByClienteAsync(string cliente)
        {
            return await Context.Set<CuentaCorriente>()
                .Where(c => c.Idcliente == cliente)
                .GroupBy(c => new { c.Idcliente, c.Empresaaplicacion,c.Formularioaplicacion, c.Numeroformularioaplicacion, c.Fechavencimiento})
                .Where(c=> c.Sum( c=> c.Importenacional) != 0)
                .Select(pendiente => new CuentaCorriente()
                {
                    Empresaaplicacion = pendiente.Key.Empresaaplicacion,
                    Formularioaplicacion = pendiente.Key.Formularioaplicacion,
                    Numeroformularioaplicacion = pendiente.Key.Numeroformularioaplicacion,
                    Fechavencimiento = pendiente.Key.Fechavencimiento,
                    Importenacional = pendiente.Sum(c=> c.Importenacional)

                })
                .ToListAsync();
        }

    }
}
