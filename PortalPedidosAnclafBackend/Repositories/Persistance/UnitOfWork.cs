using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public PortalPedidosAnclaflexContext Context { get; }
        private IClienteRepository _clientes { get; set; }

        private IClienteDireccionEntregaRepository _clientedireccionesentrega { get;  set; }

        private IPedidoRepository _pedidos { get;  set; }

        private IPedidoItemRepository _pedidositems { get; set; }

        private IPresupuestoRepository _presupuestos { get; set; }

        private IPresupuestoItemRepository _presupuestositems { get; set; }


        private IProductoRepository _productos { get;  set; }

        private IProvinciaRepository _provincias { get; set; }

        private IUsuarioRepository _usuarios{ get; set; }

        private ITransportistaRedespachoRepository _transportistasRedespacho { get; set; }
        private IBonificacionRepository _bonificaciones { get; set; }
        private IVendedorRepository _vendedores { get; set; }
        private IListaDePrecioRepository _listasDePrecio { get; set; }
        private ICuentaCorrienteRepository _cuentaCorriente{ get; set; }
        public UnitOfWork(PortalPedidosAnclaflexContext context)
        {
            Context = context;
        }

        public IClienteRepository Clientes { 
            get
            {
                    if (_clientes == null)
                    {
                        _clientes = new ClienteRepository(Context);
                    }
                    return _clientes;
            }
        }

        public IClienteDireccionEntregaRepository ClienteDireccionesEntrega
        {
            get
            {
                if (_clientedireccionesentrega == null)
                {
                    _clientedireccionesentrega = new ClienteDireccionEntregaRepository(Context);
                }
                return _clientedireccionesentrega;
            }
        }

        public IPedidoRepository Pedidos
        {
            get
            {
                if (_pedidos == null)
                {
                    _pedidos = new PedidoRepository(Context);
                }
                return _pedidos;
            }
        }

        public IPedidoItemRepository PedidosItems
        {
            get
            {
                if (_pedidositems == null)
                {
                    _pedidositems = new PedidoItemRepository(Context);
                }
                return _pedidositems;
            }
        }


        public IPresupuestoRepository Presupuestos
        {
            get
            {
                if (_presupuestos == null)
                {
                    _presupuestos = new PresupuestoRepository(Context);
                }
                return _presupuestos;
            }
        }

        public IPresupuestoItemRepository PresupuestosItems
        {
            get
            {
                if (_presupuestositems == null)
                {
                    _presupuestositems = new PresupuestoItemRepository(Context);
                }
                return _presupuestositems;
            }
        }

        public IProductoRepository Productos
        {
            get
            {
                if (_productos == null)
                {
                    _productos = new ProductoRepository(Context);
                }
                return _productos;
            }
        }

        public IProvinciaRepository Provincias
        {
            get
            {
                if (_provincias== null)
                {
                    _provincias= new ProvinciaRepository(Context);
                }
                return _provincias;
            }
        }

        public IUsuarioRepository Usuarios
        {
            get
            {
                if (_usuarios == null)
                {
                    _usuarios = new UsuarioRepository(Context);
                }
                return _usuarios;
            }
        }

        public ITransportistaRedespachoRepository TransportistaRedespacho
        {
            get
            {
                if (_transportistasRedespacho == null)
                {
                    _transportistasRedespacho = new TransportistaRedespachoRepository(Context);
                }
                return _transportistasRedespacho;
            }
        }

        public IBonificacionRepository Bonificaciones
        {
            get
            {
                if (_bonificaciones == null)
                {
                    _bonificaciones = new BonificacionRepository(Context);
                }
                return _bonificaciones;
            }
        }

        public IVendedorRepository Vendedores
        {
            get
            {
                if (_vendedores == null)
                {
                    _vendedores = new VendedorRepository(Context);
                }
                return _vendedores;
            }
        }

        public IListaDePrecioRepository ListasDePrecio
        {
            get
            {
                if (_listasDePrecio == null)
                {
                    _listasDePrecio = new ListaDePrecioRepository(Context);
                }
                return _listasDePrecio;
            }
        }

        public ICuentaCorrienteRepository CuentaCorriente
        {
            get
            {
                if (_cuentaCorriente == null)
                {
                    _cuentaCorriente  = new CuentaCorrienteRepository (Context);
                }
                return _cuentaCorriente;
            }
        }
        public async Task<int> Complete()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
