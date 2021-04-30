using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class TransportistaRedespachoRepository : RepositoryBase<Transportistasredespacho>, ITransportistaRedespachoRepository
    {
        public TransportistaRedespachoRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }

        public async Task<IEnumerable<Transportistasredespacho>> GetByTermino(string termino, int skip, int take)
        {
            //return await Context.Set<TransportistaRedespacho>().Where(c => c.Descripcion.ToUpper().Contains(termino.ToUpper()) ||
            //                                                 c.Id.ToUpper().Contains(termino.ToUpper())).Skip(skip).Take(take).ToListAsync();

            string[] palabras = termino.Split(' ');
            string query = "SELECT * FROM transportistasredespacho where 1=1 ";
            foreach (var palabra in palabras)
            {
                query = query + $"and ((UPPER(id) like '%{palabra.ToUpper()}%') or (UPPER(descripcion) like '%{palabra.ToUpper()}%'))";
            }
            return await Context.Set<Transportistasredespacho>().FromSqlRaw(query).Skip(skip).Take(take).ToListAsync();
        }

        public virtual async Task<Transportistasredespacho> Get(string id) => await Context.Set<Transportistasredespacho>().FindAsync(id);
    }
}
