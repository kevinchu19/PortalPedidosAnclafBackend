using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IProvinciaRepository : IRepository<Provincia>
    {
        Task<IEnumerable<Provincia>> GetByTermino(string termino, int skip, int take);
        Task<Provincia> Get(string id);
    }
}
