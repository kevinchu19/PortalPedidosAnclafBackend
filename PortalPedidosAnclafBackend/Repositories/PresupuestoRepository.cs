using Microsoft.AspNetCore.JsonPatch;
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
    public class PresupuestoRepository :   RepositoryBase<Presupuesto>, IPresupuestoRepository
    {
        public PresupuestoRepository(PortalPedidosAnclaflexContext context): base(context)
        {}

        public async Task ActualizaPresupuestoTransferido(int id, short nuevoEstado)
        {
            Presupuesto presupuesto = await this.Get(id);
            presupuesto.Transferido = nuevoEstado;
        }

        public async Task<Presupuesto> ActualizarEstado(int idPresupuesto, string nuevoEstado)
        {
            Presupuesto pedidoAActualizar = await this.Get(idPresupuesto);
            pedidoAActualizar.Estado = nuevoEstado;
            return pedidoAActualizar;
        }

        public async Task<Presupuesto> GetById(int id) => await Context.Set<Presupuesto>().Include(i => i.Items).ThenInclude(p => p.IdProductoNavigation)
                                                                                .Include(e => e.IdEntregaNavigation)
                                                                                .Include(e => e.ProvinciaEntregaNavigation)
                                                                                .Include(c => c.Cliente).ThenInclude(p => p.ProvinciaFacturacionNavigation)
                                                                                .Where(p=> p.Id == id)                                                                    
                                                                                .FirstOrDefaultAsync();

        public async Task<PagedList<Presupuesto>> GetByParameters(string idCliente, string idVendedor, string idPresupuesto, 
                                                             string fechaDesde, string fechaHasta, PaginationParameters parameters)
        {
            return await PagedList<Presupuesto>.ToPagedList(Context.Set<Presupuesto>()
                .Where(c => ((c.IdCliente == idCliente || 
                                idCliente == "" ||
                                idCliente == null) && 
                                (c.IdVendedor== idVendedor|| 
                                idVendedor == "" ||
                                idVendedor == null) && 
                                (c.Id.ToString() == idPresupuesto ||
                                idPresupuesto == "" ||
                                idPresupuesto == "0" ||
                                idPresupuesto == "null" ||
                                idPresupuesto == null)) &&
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
                .OrderByDescending(c => c.Id),
                parameters.PageNumber,
                parameters.PageSize);
        }


        public async Task<IEnumerable<Presupuesto>> GetByParametersForTF(string idCliente, string idVendedor, string fechaDesde, string fechaHasta)
        {
            return await Context.Set<Presupuesto>()
                .Where(c => ((c.IdCliente == idCliente ||
                                idCliente == "" ||
                                idCliente == null) &&
                                (c.IdVendedor == idVendedor ||
                                idVendedor == "" ||
                                idVendedor == null)&&
                                 (c.Fecha >= Convert.ToDateTime(fechaDesde) ||
                                fechaDesde == "" ||
                                fechaDesde == null) &&
                                (c.Fecha <= Convert.ToDateTime(fechaHasta) ||
                                fechaHasta == "" ||
                                fechaHasta == null)))
                .Include(i => i.Items).ThenInclude(p => p.IdProductoNavigation)
                .Include(c => c.Cliente)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }


        public async Task<IEnumerable<Presupuesto>> GetForSoftland(int skip, int take) => await Context.Set<Presupuesto>()
                                                                                .Select(c => new Presupuesto()
                                                                                {
                                                                                    Id = c.Id,
                                                                                    IdCliente = Convert.ToInt32(c.IdCliente)
                                                                                                .ToString()
                                                                                                .PadLeft(6, ' '),
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
                                                                                    Items = c.Items,
                                                                                    Acopio = c.Acopio,
                                                                                    DireccionModificada = c.DireccionModificada,
                                                                                    IdUsuario = c.IdUsuario
                                                                                })
                                                                                .Where(c=> c.Transferido == 0)
                                                                                .Skip(skip)
                                                                                .Take(take)
                                                                                .ToListAsync();
    }
}
