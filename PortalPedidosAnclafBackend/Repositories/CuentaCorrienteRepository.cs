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

        public async Task<ICollection<object>> GetByClienteAsync(string cliente, string idVendedor, string fechaDesde, string fechaHasta)
        {
            var a =  await Context.Set<CuentaCorrienteDTO>().Join(Context.Set<Cliente>(),
                                                             cta => cta.Idcliente,
                                                             cliente => cliente.Id,
                                                             (cta, cliente) => new CuentaCorrienteDTO(){
                                                                 Empresa=cta.Empresa,
                                                                 CodigoFormulario = cta.Codigoformulario,
                                                                 cta.Numeroformulario,
                                                                 cta.Empresaaplicacion,
                                                                 cta.Formularioaplicacion,
                                                                 cta.Numeroformularioaplicacion,
                                                                 cta.Cuota,
                                                                 cta.Fechamovimiento,
                                                                 cta.Fechavencimiento,
                                                                 cta.IdVendedor,
                                                                 cta.Importenacional,
                                                                 cta.Importeextranjera,
                                                                 cta.Idcliente,
                                                                 cliente.RazonSocial
                                                             }).Where(c => (c.Idcliente == cliente ||
                                                                    cliente == "" ||
                                                                    cliente == null) &&
                                                                    (c.IdVendedor == idVendedor ||
                                                                    idVendedor == "" ||
                                                                    idVendedor == null)
                                                              && c.Fechamovimiento >= Convert.ToDateTime(fechaDesde)
                                                              && c.Fechamovimiento <= Convert.ToDateTime(fechaHasta)).ToListAsync();
            return (ICollection<object>)a;
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
                    Importenacional = pendiente.Sum(c=> c.Importenacional)

                })
                .ToListAsync();
        }

    }
}
