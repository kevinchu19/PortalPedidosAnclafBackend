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

        IPresupuestoRepository Presupuestos { get; }
        IPresupuestoItemRepository PresupuestosItems { get; }
        IProductoRepository Productos { get; }
        IProvinciaRepository Provincias { get; }
        IUsuarioRepository Usuarios { get; }
        ITransportistaRedespachoRepository TransportistaRedespacho { get; }
        IBonificacionRepository Bonificaciones { get; }
        IVendedorRepository Vendedores { get; }
        IListaDePrecioRepository ListasDePrecio { get; }
        ICuentaCorrienteRepository CuentaCorriente { get; }
        Task<int> Complete();
    }
}
