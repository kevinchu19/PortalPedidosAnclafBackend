using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Helpers;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class PedidoRepository :   RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(PortalPedidosAnclaflexContext context): base(context)
        {}

        public async Task<PagedList<Pedido>> GetByParameters(string idCliente, string idVendedor, string idPedido, 
                                                             string fechaDesde, string fechaHasta, PaginationParameters parameters)
        {
            return await PagedList<Pedido>.ToPagedList(Context.Set<Pedido>()
                .Where(c => ((c.IdCliente == idCliente || 
                                idCliente == "" ||
                                idCliente == null) && 
                                (c.IdVendedor== idVendedor|| 
                                idVendedor == "" ||
                                idVendedor == null) && 
                                (c.Id.ToString() == idPedido ||
                                idPedido == "" ||
                                idPedido == "0" ||
                                idPedido == "null" ||
                                idPedido == null)) &&
                                (c.Fecha >= Convert.ToDateTime(fechaDesde) ||
                                fechaDesde == "" ||
                                fechaDesde == null) &&
                                (c.Fecha <= Convert.ToDateTime(fechaHasta) ||
                                fechaHasta == "" ||
                                fechaHasta == null))
                .Include(i => i.Items).ThenInclude(p=>p.IdProductoNavigation)
                .Include(e=> e.IdEntregaNavigation)
                .Include(e => e.ProvinciaEntregaNavigation)
                .Include(c => c.Cliente).ThenInclude(p => p.ProvinciaFacturacionNavigation)
                .OrderBy(c => c.Id),
                parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<IEnumerable<Pedido>> GetForSoftland(int skip, int take) => await Context.Set<Pedido>()
                                                                                .Select(c => new Pedido()
                                                                                {
                                                                                    Id = c.Id,
                                                                                    IdCliente = c.IdCliente.Replace("0"," "),
                                                                                    IdEntrega = c.IdEntrega,
                                                                                    DireccionEntrega = c.DireccionEntrega,
                                                                                    PaisEntrega = c.PaisEntrega,
                                                                                    CodigoPostalEntrega = c.CodigoPostalEntrega,
                                                                                    ProvinciaEntrega = c.ProvinciaEntrega,
                                                                                    ListaPrecios = c.ListaPrecios,
                                                                                    TransportistaRedespacho = c.TransportistaRedespacho,
                                                                                    Observacion = c.Observacion,
                                                                                    ObservacionLogistica = c.ObservacionLogistica,
                                                                                    RetiradeFabrica = c.RetiradeFabrica,
                                                                                    EsBarrioCerrado = c.EsBarrioCerrado,
                                                                                    IdVendedor = c.IdVendedor,
                                                                                    Fecha = c.Fecha,
                                                                                    Telefono = c.Telefono,
                                                                                    Email = c.Email,
                                                                                    FechaDeEntrega = c.FechaDeEntrega,
                                                                                    PagoEnEfectivo = c.PagoEnEfectivo,
                                                                                    Transferido = c.Transferido,
                                                                                    Items = c.Items
                                                                                })
                                                                                .Where(c=> c.Transferido == 0)
                                                                                .Skip(skip)
                                                                                .Take(take)
                                                                                .ToListAsync();
        
    }
}
