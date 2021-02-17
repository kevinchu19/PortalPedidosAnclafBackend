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
    }
}
