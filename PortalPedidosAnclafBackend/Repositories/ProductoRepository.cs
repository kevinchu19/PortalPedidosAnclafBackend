using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        public ProductoRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }

        public async Task<IEnumerable<Producto>> GetByTermino(string termino, int skip, int take)
        {
            return await Context.Set<Producto>().Where(c => c.Descripcion.ToUpper().Contains(termino.ToUpper())).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Producto> GetByIdYListaPrecios(string id, string listaPrecios)
        {
            return await Context.Set<Producto>()
                                            .Where(producto => producto.Id == id)
                                            .Select(producto => new Producto()
                                            {
                                                Id = producto.Id,
                                                Descripcion = producto.Descripcion,
                                                Precio = Context.Set<Listasdeprecio>()
                                                        .Where(preciolista => preciolista.Idproducto == producto.Id &&
                                                                              preciolista.Fecha <= DateTime.Now &&
                                                                              preciolista.Id == listaPrecios)
                                                        .OrderByDescending(c=> c.Fecha)
                                                        .Take(1)
                                                        .Select(c => (c.Precio))
                                                        .FirstOrDefault(),
                                                Bonificacion = 0
                                            })   
                                            .FirstOrDefaultAsync();
        }
    }
}
