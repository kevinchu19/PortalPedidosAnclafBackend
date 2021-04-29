using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll(int skip, int take);
        
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Detach(TEntity entity);
    }
}
