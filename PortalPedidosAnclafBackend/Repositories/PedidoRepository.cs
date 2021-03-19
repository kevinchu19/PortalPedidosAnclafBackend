using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class PedidoRepository :   RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(PortalPedidosAnclaflexContext context): base(context)
        {}
        
        public async Task<IEnumerable<Pedido>> GetByIdCliente(string idCliente, int skip, int take)
        {
            return await Context.Set<Pedido>()
                .Where(c => (c.IdCliente == idCliente))
                .Include(i => i.Items).Skip(skip).Take(take).ToListAsync();
        }

    }
}
