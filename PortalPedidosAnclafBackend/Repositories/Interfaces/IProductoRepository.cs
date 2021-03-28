using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<IEnumerable<Producto>> GetByTermino(string termino, int skip, int take);
        Task<ProductoDTO> GetByIdYListaPrecios(string id, string listaPrecios, string grupoBonificacion);
        Task<IEnumerable<Producto>> GetByTerminoForOrder(string termino, int skip, int take, string listaPrecios, string cliente);
    }


}
