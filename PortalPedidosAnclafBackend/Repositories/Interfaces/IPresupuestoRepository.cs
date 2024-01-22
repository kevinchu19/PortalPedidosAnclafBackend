using Microsoft.AspNetCore.JsonPatch;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IPresupuestoRepository: IRepository<Presupuesto>
    {
        Task<PagedList<Presupuesto>> GetByParameters(string idCliente,string idVendedor, string idPresupuesto, string fechaDesde, string fechaHasta, PaginationParameters parameters);
        Task<IEnumerable<Presupuesto>> GetForSoftland (int skip, int take);
        Task ActualizaPresupuestoTransferido(int id, short nuevoEstado);
        Task<Presupuesto> GetById(int id);
        Task<IEnumerable<Presupuesto>> GetByParametersForTF(string idCliente, string idVendedor, string fechaDesde, string fechaHasta);
        Task<Presupuesto> ActualizarEstado(int idPresupuesto, string nuevoEstado);
    }
}
