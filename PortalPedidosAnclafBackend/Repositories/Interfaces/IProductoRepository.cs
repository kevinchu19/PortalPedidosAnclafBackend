using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<IEnumerable<Producto>> GetByTermino(string termino, int skip, int take);
        Task<Producto> GetByIdYListaPrecios(string id, string listaPrecios);

    }


}
