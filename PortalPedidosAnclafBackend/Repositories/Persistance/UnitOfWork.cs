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

        private IProductoRepository _productos { get;  set; }

        private IProvinciaRepository _provincias { get; set; }


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
