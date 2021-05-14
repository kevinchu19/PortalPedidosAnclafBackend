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
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        public ProductoRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }

        public async Task<IEnumerable<Producto>> GetByTermino(string termino, int skip, int take)
        {
            string[] palabras = termino.Split(' ');
            string query = "SELECT * FROM productos where activo = 1  ";
            foreach (var palabra in palabras)
            {
                query = query + $"and ((UPPER(id) like '%{palabra.ToUpper()}%') or (UPPER(descripcion) like '%{palabra.ToUpper()}%'))";
            }
            
            return await Context.Set<Producto>().FromSqlRaw(query).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetByTerminoForOrder(string termino, int skip, int take, string listaPrecios, string cliente)
        {
            termino = String.IsNullOrEmpty(termino) ? "" : termino;
            string[] palabras = termino.Split(' ');
            string query = "SELECT * FROM productos where activo = 1 ";
            
            query += $" AND IFNULL((SELECT Precio FROM listasdeprecio";
            query += $" WHERE idproducto = productos.id ";
            query += $" AND listasdeprecio.id = '{listaPrecios}'";
            query += $" AND listasdeprecio.fecha <= now()";
            query += $" ORDER BY listasdeprecio.fecha DESC LIMIT 1 ),0) <> 0 ";
            
            query += $" AND (ClienteExclusivo is null OR ClienteExclusivo = '{cliente}')";
            foreach (var palabra in palabras)
            {
                query = query + $"and ((UPPER(id) like '%{palabra.ToUpper()}%') or (UPPER(descripcion) like '%{palabra.ToUpper()}%'))";
            }

            return await Context.Set<Producto>().FromSqlRaw(query).Skip(skip).Take(take).ToListAsync();
        }


        public async Task<ProductoDTO> GetByIdYListaPrecios(string id, string listaPrecio, string grupoBonificacion)
        {
            return await Context.Set<Producto>()
                                
                                            .Where(producto => producto.Id == id)
                                            .Select(producto => new ProductoDTO()
                                            {
                                                Id = producto.Id,
                                                Descripcion = producto.Descripcion,
                                                Precio = Context.Set<Listasdeprecio>()
                                                        .Where(preciolista => preciolista.Idproducto == producto.Id &&
                                                                              preciolista.Fecha <= DateTime.Now &&
                                                                              preciolista.Id == listaPrecio)
                                                        .OrderByDescending(c=> c.Fecha)
                                                        .Take(1)
                                                        .Select(c => (c.Precio!=null?c.Precio:0))
                                                        .FirstOrDefault(),
                                                Bonificacion1 = Context.Set<Bonificacion>()
                                                                        .Where(bonificacion => bonificacion.Idgrupobonificacion == grupoBonificacion &&
                                                                                                        bonificacion.Tipoproducto == producto.TipoProducto &&
                                                                                                        (bonificacion.Idnumerorubro == 1 && bonificacion.Valorrubro == producto.Rubro1 ||
                                                                                                         bonificacion.Idnumerorubro == 2 && bonificacion.Valorrubro == producto.Rubro2
                                                                                                        ))
                                                                        .Take(1)
                                                                        .Select(c => (c.Bonificacion1 != null ? c.Bonificacion1: 0))
                                                                        .FirstOrDefault(),
                                                Bonificacion2 = Context.Set<Bonificacion>()
                                                                        .Where(bonificacion => bonificacion.Idgrupobonificacion == grupoBonificacion &&
                                                                                                        bonificacion.Tipoproducto == producto.TipoProducto &&
                                                                                                        (bonificacion.Idnumerorubro == 1 && bonificacion.Valorrubro == producto.Rubro1 ||
                                                                                                         bonificacion.Idnumerorubro == 2 && bonificacion.Valorrubro == producto.Rubro2
                                                                                                        ))
                                                                        .Take(1)
                                                                        .Select(c => (c.Bonificacion2 != null ? c.Bonificacion2 : 0))
                                                                        .FirstOrDefault(),
                                                Bonificacion3 = Context.Set<Bonificacion>()
                                                                        .Where(bonificacion => bonificacion.Idgrupobonificacion == grupoBonificacion &&
                                                                                                        bonificacion.Tipoproducto == producto.TipoProducto &&
                                                                                                        (bonificacion.Idnumerorubro == 1 && bonificacion.Valorrubro == producto.Rubro1 ||
                                                                                                         bonificacion.Idnumerorubro == 2 && bonificacion.Valorrubro == producto.Rubro2
                                                                                                        ))
                                                                        .Take(1)
                                                                        .Select(c => (c.Bonificacion3 != null ? c.Bonificacion3 : 0))
                                                                        .FirstOrDefault()
                                            })   
                                            .FirstOrDefaultAsync();
        }

        public virtual async Task<Producto> Get(string id) => await Context.Set<Producto>().FindAsync(id);

    }
}
