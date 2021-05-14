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

        public async Task<IEnumerable<Cliente>> GetByTerminoAndKeyParameter(string termino, string keyParameter, int skip, int take)
        {
            //return await Context.Set<Cliente>().Where(c => c.RazonSocial.ToUpper().Contains(termino.ToUpper()) ||
            //                                               c.Id.ToUpper().Contains(termino.ToUpper())).Skip(skip).Take(take).ToListAsync();
            termino = String.IsNullOrEmpty(termino) ? "" : termino;
            string[] palabras = termino.Split(' ');
            string query = $"SELECT * FROM clientes where activo = 1 And (UPPER(idvendedor) = '{keyParameter}' Or '{keyParameter}'='') ";
            foreach (var palabra in palabras)
            {
                query = query + $"and ((UPPER(id) like '%{palabra.ToUpper()}%') or (UPPER(razonsocial) like '%{palabra.ToUpper()}%'))";
            }
            return await Context.Set<Cliente>().FromSqlRaw(query).Skip(skip).Take(take).ToListAsync();

        }

        public async Task<Cliente> GetString(string id)
        {
            return await Context.Set<Cliente>().FindAsync(id);
        }
    }
}
