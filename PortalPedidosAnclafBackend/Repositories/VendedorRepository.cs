using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class VendedorRepository : RepositoryBase<Vendedores>, IVendedorRepository
    {
        public VendedorRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }

        public virtual async Task<Vendedores> Get(string id) => await Context.Set<Vendedores>().FindAsync(id);
    }
}