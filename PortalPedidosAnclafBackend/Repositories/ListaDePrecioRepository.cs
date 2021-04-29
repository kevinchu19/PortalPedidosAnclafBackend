using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class ListaDePrecioRepository : RepositoryBase<Listasdeprecio>, IListaDePrecioRepository
    {
        public ListaDePrecioRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }
        public async Task<Listasdeprecio> Get(string idLista, string idProducto, DateTime fecha)
        {
            return await Context.Set<Listasdeprecio>().FindAsync(new object[] { idLista, idProducto, fecha });
        }


    }
}