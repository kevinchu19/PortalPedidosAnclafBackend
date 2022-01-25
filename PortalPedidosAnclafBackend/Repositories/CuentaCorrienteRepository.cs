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

    }
}
