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
        public async Task<IEnumerable<Clientesdireccionesentrega>> GetByTerminoAndKeyParameter(string termino, string keyParameter, int skip, int take)
        {

            //return await Context.Set<Clientesdireccionesentrega>().Where(c => (termino == null || 
            //                                                                   c.Descripcion.ToUpper().Contains(termino.ToUpper())||
            //                                                                   c.Id.ToUpper().Contains(termino.ToUpper()) ) &&
            //                                                                  c.IdCliente == keyParameter.ToUpper())
            //                                                     .Skip(skip).Take(take).ToListAsync();

            termino = String.IsNullOrEmpty(termino) ? "" : termino;
            string[] palabras = termino.Split(' ');
            string query = $"SELECT * FROM clientesdireccionesentrega where UPPER(idcliente) = '{keyParameter.ToUpper()}'";
            foreach (var palabra in palabras)
            {
                query = query + $"and ((UPPER(id) like '%{palabra.ToUpper()}%') or (UPPER(descripcion) like '%{palabra.ToUpper()}%'))";
            }
            return await Context.Set<Clientesdireccionesentrega>().FromSqlRaw(query).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Clientesdireccionesentrega> GetByIdAndKeyParameter(string id, string keyParameter)
        {
            return await Context.Set<Clientesdireccionesentrega>().Where(c => (c.Id == id &&
                                                                               c.IdCliente == keyParameter))
                                                                  .FirstOrDefaultAsync();
        }

        public async Task<Clientesdireccionesentrega> Get(string idCliente, string id)
        {
            return await Context.Set<Clientesdireccionesentrega>().FindAsync(new object[] { idCliente, id});
        }

    }
}