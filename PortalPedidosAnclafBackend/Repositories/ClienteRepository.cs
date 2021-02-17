using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class ClienteRepository: RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(PortalPedidosAnclaflexContext context): base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> GetByTermino(string termino, int skip, int take)
        {
            return await Context.Set<Cliente>().Where(c => c.RazonSocial.ToUpper().Contains(termino.ToUpper())).Skip(skip).Take(take).ToListAsync();
        }
    }
}
