using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public class ClienteDireccionEntregaRepository : RepositoryBase<Clientesdireccionesentrega>, IClienteDireccionEntregaRepository
    {
        public ClienteDireccionEntregaRepository(PortalPedidosAnclaflexContext context) : base(context)
        {
        }
    }
}
