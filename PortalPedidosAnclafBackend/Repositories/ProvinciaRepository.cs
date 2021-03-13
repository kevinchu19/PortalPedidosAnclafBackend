using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class ProvinciaRepository : RepositoryBase<Provincia>, IProvinciaRepository
    {
        public ProvinciaRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }

        public async Task<IEnumerable<Provincia>> GetByTermino(string termino, int skip, int take)
        {
            return await Context.Set<Provincia>().Where(c => c.Descripcion.ToUpper().Contains(termino.ToUpper())).Skip(skip).Take(take).ToListAsync();
        }
    }
}
