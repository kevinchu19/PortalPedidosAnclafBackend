﻿using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class ProvinciaRepository : RepositoryBase<Provincia>, IProvinciaRepository
    {
        public ProvinciaRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }

        public async Task<IEnumerable<Provincia>> GetByTermino(string termino, int skip, int take)
        {
            //return await Context.Set<Provincia>().Where(c => c.Descripcion.ToUpper().Contains(termino.ToUpper()) ||
            //                                                 c.Id.ToUpper().Contains(termino.ToUpper())).Skip(skip).Take(take).ToListAsync();
            termino = String.IsNullOrEmpty(termino) ? "" : termino;
            string[] palabras = termino.Split(' ');
            string query = "SELECT * FROM provincias where activo = 1  ";
            foreach (var palabra in palabras)
            {
                query = query + $"and ((UPPER(id) like '%{palabra.ToUpper()}%') or (UPPER(descripcion) like '%{palabra.ToUpper()}%'))";
            }
            return await Context.Set<Provincia>().FromSqlRaw(query).Skip(skip).Take(take).ToListAsync();
        }

        public virtual async Task<Provincia> Get(string id) => await Context.Set<Provincia>().FindAsync(id);
    }
}
