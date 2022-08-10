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
        public string[] codigosInternosExcluidos = new string[] { "DI", "CI", "AP", "AAP", "SA", "RCP", "ARC", "SD" };
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
                                                              && c.Fechamovimiento <= Convert.ToDateTime(fechaHasta)
                                                              && !codigosInternosExcluidos.Contains(c.Codigoformulario)
                                                              && c.Empresa == "ANCLAF01"
                                                              )
                
                .OrderBy(c => c.Fechamovimiento)
                .GroupBy(c => new { c.Idcliente, c.Fechamovimiento, c.Empresa, c.Codigoformulario, c.Numeroformulario, c.PdfPath })
                .Select(historico => new CuentaCorriente()
                {
                    Empresa = historico.Key.Empresa,
                    Codigoformulario = historico.Key.Codigoformulario,
                    Numeroformulario = historico.Key.Numeroformulario,
                    Fechavencimiento = Context.Set<CuentaCorriente>().Where(c => c.Empresa == historico.Key.Empresa
                                                                            && c.Codigoformulario == historico.Key.Codigoformulario
                                                                            && c.Numeroformulario == historico.Key.Numeroformulario
                                                                            //&& c.Codigoformulario == c.Formularioaplicacion
                                                                            //&& c.Numeroformulario == c.Numeroformularioaplicacion
                                                                            )
                                                                            .Select(c => c.Fechavencimiento)
                                                                            .First(),
                    PdfPath = historico.Key.PdfPath,
                    Fechamovimiento = historico.Key.Fechamovimiento,
                    Importenacional = historico.Sum(c => c.Importenacional)//,
                    //IdClienteNavigation = Context.Set<Cliente>().Where(c => c.Id == historico.Key.Idcliente).First()

                })
                .ToListAsync()
                ;
            
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
                    //&& c.Fechamovimiento >= Convert.ToDateTime(fechaDesde)
                    //&& c.Fechamovimiento <= Convert.ToDateTime(fechaHasta)
                    && !codigosInternosExcluidos.Contains(c.Formularioaplicacion)
                    && c.Empresaaplicacion == "ANCLAF01")
                .OrderBy(c => c.Idcliente).ThenBy(c => c.Fechavencimiento)
                .GroupBy(c => new { c.Idcliente, c.Fechavencimiento, c.Empresaaplicacion, c.Formularioaplicacion, c.Numeroformularioaplicacion})
                .Where(c => c.Sum(c => c.Importenacional) != 0)
                .Select(pendiente => new CuentaCorriente()
                {
                    Empresa = pendiente.Key.Empresaaplicacion,
                    Codigoformulario = pendiente.Key.Formularioaplicacion.Replace("AC","RC"),
                    Numeroformulario = pendiente.Key.Numeroformularioaplicacion,
                    Fechavencimiento = pendiente.Key.Fechavencimiento,
                    PdfPath = Context.Set<CuentaCorriente>().Where(c => c.Empresa == pendiente.Key.Empresaaplicacion
                                                                            && c.Codigoformulario == pendiente.Key.Formularioaplicacion.Replace("AC", "RC")
                                                                            && c.Numeroformulario == pendiente.Key.Numeroformularioaplicacion
                                                                            && (c.Codigoformulario == c.Formularioaplicacion || c.Codigoformulario =="RC")
                                                                            && c.Numeroformulario == c.Numeroformularioaplicacion)
                                                                    .Select(c => c.PdfPath)
                                                                    .First(),
                    Fechamovimiento = Context.Set<CuentaCorriente>().Where(c => c.Empresa == pendiente.Key.Empresaaplicacion
                                                                            && c.Codigoformulario == pendiente.Key.Formularioaplicacion.Replace("AC", "RC")
                                                                            && c.Numeroformulario == pendiente.Key.Numeroformularioaplicacion
                                                                            && (c.Codigoformulario == c.Formularioaplicacion || c.Codigoformulario == "RC")
                                                                            && c.Numeroformulario == c.Numeroformularioaplicacion)
                                                                    .Select(c => c.Fechamovimiento != null ? c.Fechamovimiento : c.Fechavencimiento)
                                                                    .First(),
                    Importenacional = pendiente.Sum(c => c.Importenacional),
                    IdClienteNavigation = Context.Set<Cliente>().Where(c => c.Id == pendiente.Key.Idcliente).First()
                    
                })
                
                .ToListAsync();
        }

    }
}
