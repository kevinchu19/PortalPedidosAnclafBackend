using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class CuentaCorrienteRepository : RepositoryBase<CuentaCorriente>, ICuentaCorrienteRepository
    {
        public CuentaCorrienteRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }


        public async Task<CuentaCorriente> Get(CuentaCorriente primaryKey)
        {
            return await Context.Set<CuentaCorriente>().FindAsync(primaryKey.Empresa,primaryKey.Codigoformulario, primaryKey.Numeroformulario,
                primaryKey.Empresaaplicacion, primaryKey.Formularioaplicacion,primaryKey.Numeroformularioaplicacion, 
                primaryKey.Cuota);
        }

        public async Task<ICollection<CuentaCorriente>> GetByClienteAsync(string cliente, string idVendedor, string fechaDesde, string fechaHasta)
        {
            return  await Context.Set<CuentaCorriente>().Where(c => (c.Idcliente == cliente ||
                                                                    cliente == "" ||
                                                                    cliente == null) &&
                                                                    (c.IdVendedor == idVendedor ||
                                                                    idVendedor == "" ||
                                                                    idVendedor == null)
                                                              && c.Fechamovimiento >= Convert.ToDateTime(fechaDesde)
                                                              && c.Fechamovimiento <= Convert.ToDateTime(fechaHasta))
                .Include( c=> c.IdClienteNavigation)
                .ToListAsync();
            
        }

        public async Task<ICollection<CuentaCorriente>> GetPendientesByClienteAsync(string cliente, string idVendedor, string fechaDesde, string fechaHasta)
        {
            return await Context.Set<CuentaCorriente>()
                .Where(c => (c.Idcliente == cliente ||
                                cliente == "" ||
                                cliente == null) &&
                                (c.IdVendedor == idVendedor ||
                                idVendedor == "" ||
                                idVendedor == null)
                    && c.Fechamovimiento >= Convert.ToDateTime(fechaDesde)
                    && c.Fechamovimiento <= Convert.ToDateTime(fechaHasta))
                .GroupBy(c => new { c.Idcliente, c.Empresaaplicacion,c.Formularioaplicacion, c.Numeroformularioaplicacion, c.Fechavencimiento})
                .Where(c=> c.Sum( c=> c.Importenacional) != 0 )
                .Select(pendiente => new CuentaCorriente()
                {
                    Empresa = pendiente.Key.Empresaaplicacion,
                    Codigoformulario = pendiente.Key.Formularioaplicacion,
                    Numeroformulario = pendiente.Key.Numeroformularioaplicacion,
                    Fechavencimiento = pendiente.Key.Fechavencimiento,
                    Fechamovimiento = Context.Set<CuentaCorriente>().Where(c=>c.Empresa == pendiente.Key.Empresaaplicacion 
                                                                            && c.Codigoformulario == pendiente.Key.Formularioaplicacion
                                                                            && c.Numeroformulario == pendiente.Key.Numeroformularioaplicacion
                                                                            && c.Codigoformulario == c.Formularioaplicacion
                                                                            && c.Numeroformulario == c.Numeroformularioaplicacion)
                                                                    .Select(c=> c.Fechamovimiento!=null ? c.Fechamovimiento : c.Fechavencimiento)
                                                                    .First(),
                    Importenacional = pendiente.Sum(c=> c.Importenacional),
                    IdClienteNavigation = Context.Set<Cliente>().Where(c=>c.Id== pendiente.Key.Idcliente).First()
                })
                
                .ToListAsync();
        }

    }
}
