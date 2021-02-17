using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Clientesdireccionesentrega>> GetByKeyParameter(string termino, string keyParameter, int skip, int take)
        {

            return await Context.Set<Clientesdireccionesentrega>().Where(c => (termino == null || c.Descripcion.ToUpper().Contains(termino.ToUpper())) &&
                                                                              c.IdCliente == keyParameter.ToUpper())
                                                                 .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Clientesdireccionesentrega> GetByIdAndKeyParameter(string id, string keyParameter)
        {
            return await Context.Set<Clientesdireccionesentrega>().Where(c => (c.Id == id &&
                                                                               c.IdCliente == keyParameter))
                                                                  .FirstOrDefaultAsync();
        }
    }
}