using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IClienteRepository Clientes { get; }
        IClienteDireccionEntregaRepository ClienteDireccionesEntrega { get; }
        IPedidoRepository Pedidos { get; }
        IPedidoItemRepository PedidosItems { get; }
        IProductoRepository Productos { get; }
        IProvinciaRepository Provincias { get; }
        IUsuarioRepository Usuarios { get; }

        Task<int> Complete();
    }
}
