using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IPedidoRepository: IRepository<Pedido>
    {
        Task<PagedList<Pedido>> GetByParameters(string idCliente,string idVendedor, string idPedido, string fechaDesde, string fechaHasta, PaginationParameters parameters);
        Task<IEnumerable<Pedido>> GetForSoftland (int skip, int take);
        Task ActualizaPedidoTransferido(int id, short nuevoEstado);

        Task<IEnumerable<Pedido>> GetByParametersForTF(string idCliente, string idVendedor, string fechaDesde, string fechaHasta);
    }
}
