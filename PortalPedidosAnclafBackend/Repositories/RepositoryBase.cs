using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class RepositoryBase<TEntity>: IRepository<TEntity> where TEntity: class
    {
        protected readonly DbContext Context;

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }
        public virtual async Task<TEntity> Get(int id) => await Context.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAll(int skip, int take) => await Context.Set<TEntity>()
                                                                                    .Skip(skip)
                                                                                    .Take(take)
                                                                                    .ToListAsync();

        public async Task Add(TEntity entity) => await Context.Set<TEntity>().AddAsync(entity);

        public async Task AddRange(IEnumerable<TEntity> entities) => await Context.Set<TEntity>().AddRangeAsync(entities);


        public void Remove(TEntity entity) => Context.Set<TEntity>().Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().RemoveRange(entities);

        public void Update(TEntity entity) => Context.Set<TEntity>().Update(entity);
    }
}
