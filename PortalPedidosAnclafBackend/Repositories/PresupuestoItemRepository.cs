using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class PresupuestoItemRepository : RepositoryBase<Presupuestositem>, IPresupuestoItemRepository
    {
        public PresupuestoItemRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }
    }
}
